# DSA Services 與功能對應分析

## 📋 概述

本文件記錄 DSA Server 管理系統中各個 UI 功能與對應的 DSA Services 關係，包含資料來源、呼叫時機和資料流向。透過 FISCA 協議框架進行通訊。

## 🗂️ 核心 DSA Services 清單

### 1. Server.ListApplication
**用途**: 取得伺服器上所有部署的應用程式清單

**UI 功能對應**:
- 📍 `ServerManagePanel` > `School Manager` 頁籤 > `dgvSchoolManageList` 學校清單

**呼叫位置**:
- `ServerManager.ListApplication()` (第38行)
- `Connection.SendRequest("Server.ListApplication", ...)` (透過 FISCA 協議)

**呼叫時機**:
- 伺服器連線成功時：`Server.Connect()` → `LoadApplications()`
- 手動重新載入：`Server.ReloadApplications()`

**回應格式**:
```xml
<Response>
  <Application Name="school001">
    <Property Name="Enabled">true</Property>
    <Property Name="Param">
      <Application>
        <Param Name="db_url">jdbc:postgresql://127.0.0.1/school001_db</Param>
        <Param Name="db_user">school001_user</Param>
        <Param Name="db_pwd">encrypted_password</Param>
        <Param Name="db_udt_user">school001_admin</Param>
        <Param Name="db_udt_pwd">encrypted_admin_password</Param>
        <Param Name="school_code">001</Param>
        <Param Name="app_comment">測試學校</Param>
      </Application>
    </Property>
  </Application>
  <!-- 更多應用程式... -->
</Response>
```

**資料處理**:
- 解析成 `Application` 物件存入 `CurrentServer.Applications`
- 排除 `IsShared=true` 的 shared 應用程式
- 每個應用程式對應一個學校

---

### 2. Server.GetServerInfo
**用途**: 取得伺服器完整組態資訊和狀態

**UI 功能對應**:
- 📍 `ServerManagePanel` > `Server Configuration` 頁籤的所有設定顯示

**呼叫位置**:
- `ServerManager.GetServerInfo()` (第33行)
- `Connection.SendRequest("Server.GetServerInfo", ...)` (透過 FISCA 協議)

**呼叫時機**:
- 伺服器連線成功時：`Server.Connect()` → `LoadConfiguration()`
- 手動重新載入：`Server.ReloadConfiguration()`

**資料處理**:
- 解析 `Property[@Name='ServerXml']/ApplicationServer` 節點
- 透過 `ServerConfiguration.FromXml()` 載入組態
- 額外解析 `UpdateInfo` 和 `SVN/@Date` 作為更新時間

**UI 顯示項目**:
- 基本資訊：版本、更新時間、存取點 URL
- 日誌設定：LogEnabled, LogProcess, LogUDS, CompressData, Target
- 更新 URL：ServiceDefinitionUpdateUrl, ComponentUpdateUrl
- 負載平衡：LoadBalances 列表
- 資料庫版本：DBMSVersionString

---

### 3. LoadBalance.UpdateServerConfiguration
**用途**: 更新伺服器組態設定

**UI 功能對應**:
- 📍 `ServerManagePanel` > `Server Configuration` 頁籤 > 「編輯設定」功能

**呼叫位置**:
- `ServerManager.UpdateServerConfiguration()` (第50行)
- `Connection.SendRequest("LoadBalance.UpdateServerConfiguration", ...)` (透過 FISCA 協議)

**觸發方式**:
1. **直接 XML 編輯** (`btnRawEdit_Click`):
   - 開啟 XML 編輯器讓使用者編輯完整組態
   - 驗證 XML 格式後送出

2. **表單欄位編輯** (`btnSaveConfig_Click`):
   - 收集表單欄位資料 (URL設定、負載平衡)
   - 更新 `Configuration` 物件後呼叫 `ToXml()`

**資料格式**: 完整的伺服器 XML 組態

**後續動作**:
```csharp
CurrentServer.Manager.ReloadServer();    // 重新載入伺服器
CurrentServer.ReloadConfiguration();     // 重新載入組態
SetServerObject(CurrentServer);          // 刷新 UI 顯示
```

---

### 4. Database.Query
**用途**: 執行 PostgreSQL 查詢命令

**UI 功能對應**:
- 📍 資料庫清單功能 (`DatabaseListForm`)
- 📍 各種資料庫資訊查詢

**呼叫位置**:
- `ServerManager.ExecuteQueryCommand()` (第265行)
- `Connection.SendRequest("Database.Query", ...)` (透過 FISCA 協議)

**常見查詢**:
```sql
-- 列出所有資料庫
select pg_database.oid,datname,description
from pg_database left join pg_shdescription on pg_database.oid=pg_shdescription.objoid

-- 取得資料庫版本
select version();

-- 取得當前資料庫
select current_database();

-- 取得資料庫大小
select pg_database_size('{資料庫名稱}');
```

**請求格式**:
```xml
<Request>
  <Command Database="目標資料庫" UserName="使用者" Password="密碼">
    SELECT * FROM table_name;
  </Command>
</Request>
```

---

## 🔄 應用程式生命週期相關 DSA Services

### Server.CloneApplication
**用途**: 從樣板複製建立新應用程式

**呼叫位置**: `ServerManager.CloneApplication()` (第57行)

### Server.RemoveApplication
**用途**: 刪除指定的應用程式

**呼叫位置**: `ServerManager.RemoveApplication()` (第65行)

### Server.RenameApplication
**用途**: 重新命名應用程式

**呼叫位置**: `ServerManager.RenameApplication()` (第147行)

### Server.SetApplicationParam
**用途**: 設定應用程式參數

**呼叫位置**: `ServerManager.SetApplicationArgument()` (第134行)

### Server.SetApplicationEnabled
**用途**: 啟用/停用應用程式

**呼叫位置**: `ServerManager.SetApplicationEnable()` (第156行)

---

## 🔧 系統維護相關 DSA Services

### LoadBalance.UpdateServices
**用途**: 更新負載平衡服務

**呼叫位置**: `ServerManager.UpdateServices()` (第75行)

### LoadBalance.ReloadServer
**用途**: 重新載入伺服器配置

**呼叫位置**: `ServerManager.ReloadServer()` (第127行)

### Database.Command
**用途**: 執行 DML 命令 (INSERT/UPDATE/DELETE)

**呼叫位置**: `ServerManager.ExecuteUpdateCommand()` (第284行)

### Database.Commands
**用途**: 批次執行多個資料庫命令

**呼叫位置**: `ServerManager.UseCommandsService()` (第336行)
**版本需求**: DSA 4.1.7.1 以上

---

## 📊 資料流向總覽

### 學校清單載入流程
```
UI: dgvSchoolManageList
↑
ServerManagePanel.InitialConfigurationPanel()
↑
CurrentServer.Applications
↑
Server.LoadApplications()
↑
ServerManager.ListApplication()
↑
DSA Service: Server.ListApplication (透過 FISCA 協議)
```

### 伺服器設定載入流程
```
UI: Server Configuration 頁籤各欄位
↑
ServerManagePanel.InitialConfigurationPanel()
↑
CurrentServer.Configuration.*
↑
Server.LoadConfiguration()
↑
ServerManager.GetServerInfo()
↑
DSA Service: Server.GetServerInfo (透過 FISCA 協議)
```

### 設定儲存流程
```
UI: 編輯設定按鈕
↓
收集表單資料 或 XML編輯器
↓
CurrentServer.Configuration.ToXml() 或 直接XML
↓
ServerManager.UpdateServerConfiguration()
↓
DSA Service: LoadBalance.UpdateServerConfiguration (透過 FISCA 協議)
↓
ReloadServer() + ReloadConfiguration()
↓
刷新 UI 顯示
```

---

## 🎯 重要發現

1. **資料快取機制**: 學校清單和伺服器設定都在連線時一次載入，存在本地快取中
2. **事件驅動更新**: 透過事件機制同步資料變更和 UI 更新
3. **雙向資料綁定**: 設定可以透過表單或直接 XML 編輯修改
4. **版本相容性**: 某些功能需要特定的 DSA 版本支援
5. **安全機制**: 資料庫操作支援超級使用者權限控制

---

📝 **分析完成**: 基於 ServerManagePanel.cs 和相關程式碼的完整 DSA Services 對應關係