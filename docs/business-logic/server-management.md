# 伺服器管理業務邏輯分析

## 📋 概述

伺服器管理是整個 DSA Server 管理系統的核心，負責與 FISCA DSA 伺服器建立連線、管理連線狀態，以及提供各種伺服器操作的統一入口。

## 🏗️ 核心類別分析

### 1. Server 類別 (`Manager/Model/Server.cs`)

**職責**: 代表一個 DSA 伺服器實例，管理連線生命週期和基本資訊

```csharp
// 核心屬性
public class Server {
    public ServerRegistryData RegistryData { get; set; }  // 註冊資料
    internal Connection Connection { get; set; }           // FISCA 連線
    public bool IsConnected { get; set; }                 // 連線狀態
    public ServerConfiguration Configuration { get; private set; }
    public ServerManager Manager { get; private set; }    // 操作管理器
    public AccountData SuperUser { get; }                 // 超級使用者帳號

    // 版本資訊
    public string CoreVersion { get; private set; }       // DSA 核心版本
    public string ServiceVersion { get; private set; }    // 服務版本
    public string DBMSVersionString { get; private set; } // 資料庫版本
}
```

**關鍵業務流程**:

#### 1.1 伺服器連線流程
```
1. Server.Connect()
2. ├── ConnectInternal() -> 建立 FISCA Connection
3. ├── 啟用 SecureTunnel = true
4. ├── 使用 RegistryData 的憑證連線
5. ├── 建立 ServerManager 實例
6. ├── LoadConfiguration() -> 載入伺服器組態
7. ├── LoadApplications() -> 載入應用程式列表
8. ├── GetVersionsInfo() -> 取得版本資訊
9. └── IsConnected = true
```

#### 1.2 版本資訊取得
- **CoreVersion**: 從 `Server.GetApplicationInfo` 的 Header["Version"] 取得
- **ServiceVersion**: 從回應的 DeployVersion 欄位取得
- **DBMSVersionString**: 透過 DatabaseManager 執行 `select version();` 取得

### 2. ServerManager 類別 (`Manager/Model/ServerManager.cs`)

**職責**: DSA Server 的 API 操作封裝，所有與伺服器的通訊都透過這個類別

```csharp
class ServerManager {
    public Connection Connection { get; set; }
    private Server Srv { get; set; }

    // 核心 API 方法
    internal XmlElement GetServerInfo()                    // 取得伺服器資訊
    internal XmlElement ListApplication()                  // 列出應用程式
    internal XmlElement UpdateServerConfiguration()        // 更新組態
    internal void CloneApplication(string target)          // 複製應用程式
    internal void RemoveApplication(string target)         // 移除應用程式
    internal XmlElement UpdateServices(bool enforceUpdate) // 更新服務
    internal XmlElement ReloadServer()                     // 重新載入伺服器
}
```

**重要 FISCA 服務呼叫對應**:

| 業務功能 | FISCA 服務名稱 | 說明 |
|---------|---------------|------|
| 取得伺服器資訊 | `Server.GetServerInfo` | 回傳 XML 格式的伺服器完整資訊 |
| 列出應用程式 | `Server.ListApplication` | 列出部署的所有應用程式 |
| 複製應用程式 | `Server.CloneApplication` | 從樣板複製新的應用程式實例 |
| 移除應用程式 | `Server.RemoveApplication` | 刪除指定的應用程式 |
| 更新服務 | `LoadBalance.UpdateServices` | 更新負載平衡服務 |
| 重新載入 | `LoadBalance.ReloadServer` | 重新載入伺服器配置 |
| 更新組態 | `LoadBalance.UpdateServerConfiguration` | 更新伺服器設定 |

### 3. ServerRegistryData 類別 (`Manager/Model/ServerRegistryData.cs`)

**職責**: 儲存伺服器的註冊資訊和連線參數

```csharp
class ServerRegistryData {
    public string AccessPointUrl { get; set; }  // DSA 存取點 URL
    public string UserName { get; set; }        // 管理員帳號
    public string Password { get; set; }        // 管理員密碼 (加密儲存)
    public AccountData SuperUser { get; set; }  // 資料庫超級使用者
}
```

## 🔐 安全機制分析

### 3.1 連線安全
```csharp
Connection conn = new Connection();
conn.EnableSecureTunnel = true;  // 啟用加密通道
conn.Connect(AccessPointUrl, "", UserName, Password);
```

### 3.2 密碼管理
- 密碼以加密方式儲存在 `ServerRegistryData`
- 支援動態修改密碼 `ChangeConnectionPassword()`
- 修改密碼後自動重新建立連線

### 3.3 超級使用者機制
```csharp
// 資料庫操作時的帳號處理
if (Srv.SuperUser != AccountData.Default) {
    req.SetAttribute("Command", "UserName", Srv.SuperUser.UserName);
    req.SetAttribute("Command", "Password", Srv.SuperUser.Password);
}
```

## 📡 通訊協議分析

### 4.1 FISCA Envelope 結構
```csharp
// 所有 FISCA 請求都包裝在 Envelope 中
Envelope request = new Envelope(xmlHelper);
Envelope response = Connection.SendRequest(serviceName, request);
XmlElement responseBody = response.ResponseBody();
```

### 4.2 XML 資料格式
- 請求和回應都是 XML 格式
- 使用 `FISCA.XHelper` 進行 XML 操作
- 複雜的階層式資料結構

### 4.3 錯誤處理機制
```csharp
try {
    XmlElement result = Connection.SendRequest(serviceName, envelope);
    return result;
} catch (Exception ex) {
    // 統一錯誤處理和日誌記錄
    throw new DSAServerException("伺服器操作失敗", ex);
}
```

## 🔄 狀態管理

### 5.1 連線狀態追蹤
- `IsConnected` 布林值表示連線狀態
- 連線失敗時自動清理相關資源
- 支援重新連線機制

### 5.2 應用程式狀態同步
```csharp
// 應用程式異動時觸發事件
public event EventHandler<AppChangeEventArgs> ApplicationAdded;
public event EventHandler<AppChangeEventArgs> ApplicationRemoved;
```

### 5.3 組態同步機制
- `ReloadConfiguration()` 重新載入伺服器組態
- `ReloadApplications()` 重新載入應用程式列表
- 確保本地快取與伺服器狀態同步


---
📝 **分析完成**: 伺服器管理核心邏輯已完整分析，可供 Web 版本開發參考