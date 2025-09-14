# 應用程式生命週期管理分析

## 📋 概述

應用程式 (Application) 是 DSA Server 上部署的業務邏輯單元，每個應用程式都有獨立的資料庫連線、參數設定和執行狀態。系統提供完整的應用程式生命週期管理，包括建立、配置、啟用/停用、重新命名和刪除。

## 🏗️ 核心類別分析

### 1. Application 類別 (`Manager/Model/Application.cs`)

**職責**: 代表一個部署在 DSA Server 上的應用程式實例

```csharp
class Application {
    // 基本屬性
    public string Name { get; private set; }              // 應用程式名稱
    public bool Enabled { get; private set; }             // 啟用狀態
    public bool IsShared { get; private set; }            // 是否為共享應用程式
    public Server Owner { get; private set; }             // 所屬伺服器

    // 資料庫連線資訊
    public string DatabaseFullName { get; private set; }  // 完整資料庫連線字串
    public string DatabaseName { get; }                   // 資料庫名稱 (解析後)
    public string DMLUserName { get; private set; }       // 資料操作帳號
    public string DMLPassword { get; private set; }       // 資料操作密碼
    public string DDLUserName { get; private set; }       // 結構異動帳號
    public string DDLPassword { get; private set; }       // 結構異動密碼

    // 業務參數
    public string SchoolCode { get; private set; }        // 學校代碼
    public string Comment { get; private set; }           // 應用程式說明

    // 自定參數集合
    private Dictionary<string, string> parameters;
}
```

### 2. 特殊應用程式: Shared Application

**概念**: `shared` 是特殊的應用程式，提供系統級服務

```csharp
public const string SharedName = "shared";

// 識別共享應用程式
if (string.Equals(Name, SharedName, StringComparison.OrdinalIgnoreCase))
    IsShared = true;

// 取得共享應用程式實例
public Application GetSharedApplication() {
    foreach (Application each in Applications) {
        if (each.IsShared) return each;
    }
    return null;
}
```

## 🔄 生命週期操作分析

### 3.1 應用程式建立 (`Server.AddApplication`)

**流程分析**:
```
1. Server.AddApplication(string name)
2. ├── Manager.CloneApplication(name)          // 呼叫 FISCA 服務複製
3. ├── Manager.ListApplication(name)           // 取得新建立的應用程式定義
4. ├── new Application(this)                   // 建立本地應用程式物件
5. ├── app.LoadDefinition(xmlElement)          // 載入 XML 定義資料
6. ├── _apps.Add(app.Name, app)               // 加入本地快取
7. └── ApplicationAdded 事件觸發               // 通知 UI 更新
```

**FISCA 服務**: `Server.CloneApplication`
- 從系統樣板複製一個新的應用程式實例
- 自動配置基本的資料庫連線和參數

### 3.2 應用程式刪除 (`Server.RemoveApplication`)

**流程分析**:
```
1. Server.RemoveApplication(string name)
2. ├── 檢查應用程式是否存在
3. ├── Application toremove = _apps[name]      // 取得要刪除的應用程式
4. ├── Manager.RemoveApplication(name)         // 呼叫 FISCA 服務刪除
5. ├── _apps.Remove(name)                     // 從本地快取移除
6. └── ApplicationRemoved 事件觸發            // 通知 UI 更新
```

**FISCA 服務**: `Server.RemoveApplication`
- 從 DSA Server 完全移除應用程式
- 包括資料庫連線、設定檔和執行狀態

### 3.3 應用程式重新命名 (`Server.RenameApplication`)

**流程分析**:
```
1. Server.RenameApplication(string oldName, string newName)
2. ├── 檢查舊應用程式是否存在
3. ├── Manager.RenameApplication(oldName, newName)  // FISCA 服務重新命名
4. ├── Manager.ListApplication(newName)             // 取得更新後的定義
5. ├── app.LoadDefinition(responseXml)              // 重新載入定義
6. ├── _apps.Remove(oldName)                       // 從快取移除舊名稱
7. ├── _apps.Add(newName, app)                     // 以新名稱加入快取
8. └── app.RaiseNameChanged()                      // 觸發名稱變更事件
```

### 3.4 應用程式啟用/停用 (`Server.SetApplicationEnable`)

**流程分析**:
```
1. Server.SetApplicationEnable(string name, bool enable)
2. ├── 檢查應用程式是否存在
3. ├── Manager.SetApplicationEnable(name, enable)   // FISCA 服務設定狀態
4. ├── Manager.ListApplication(name)                // 取得更新後的狀態
5. ├── app.LoadDefinition(responseXml)              // 重新載入定義
6. └── app.RaiseConfigChanged()                    // 觸發配置變更事件
```

## ⚙️ 參數配置系統

### 4.1 應用程式參數結構

**標準參數**:
```csharp
// 資料庫相關參數
"db_url"      -> DatabaseFullName   // 完整資料庫連線字串
"db_user"     -> DMLUserName        // 資料操作帳號
"db_pwd"      -> DMLPassword        // 資料操作密碼
"db_udt_user" -> DDLUserName        // 結構異動帳號
"db_udt_pwd"  -> DDLPassword        // 結構異動密碼

// 業務參數
"school_code"  -> SchoolCode        // 學校代碼
"app_comment"  -> Comment           // 應用程式說明
```

**自定參數**:
```csharp
// 支援任意自定參數
private Dictionary<string, string> parameters;

// 取得所有參數 (包含標準和自定)
public Dictionary<string, string> GetParameters() {
    return new Dictionary<string, string>(parameters);
}
```

### 4.2 參數更新機制 (`Server.SetApplicationArgument`)

**單一應用程式參數更新**:
```
1. Server.SetApplicationArgument(Application.Argument arg)
2. ├── 檢查應用程式是否存在
3. ├── Manager.SetApplicationArgument(arg.ToXml())     // 轉為 XML 後送出
4. ├── Manager.ListApplication(arg.Name)               // 取得更新後的配置
5. ├── app.LoadDefinition(responseXml)                 // 重新載入定義
6. └── app.RaiseConfigChanged()                       // 觸發配置變更事件
```

**FISCA 服務**: `Server.SetApplicationParam`
- 接受 XML 格式的參數定義
- 支援批次更新多個參數

### 4.3 Argument 類別 - 參數封裝

```csharp
public class Argument : Dictionary<string, string> {
    public string Name { get; set; }  // 應用程式名稱

    // 轉換為 FISCA 所需的 XML 格式
    public XmlElement ToXml() {
        FISCA.XHelper helper = new FISCA.XHelper("<Application/>");
        helper.SetAttribute(".", "Name", Name);

        foreach (KeyValuePair<string, string> p in this) {
            FISCA.XHelper xml = new FISCA.XHelper(helper.AddElement(".", "Param", p.Value));
            xml.SetAttribute(".", "Name", p.Key);
        }

        return helper.Data;
    }
}
```

## 🗂️ XML 定義載入分析

### 5.1 LoadDefinition 方法詳解

```csharp
public void LoadDefinition(XmlElement definition) {
    FISCA.XHelper Definition = new FISCA.XHelper(definition);

    // 基本屬性載入
    Name = Definition.GetText("@Name");
    Enabled = bool.Parse(Definition.GetText("Property[@Name='Enabled']"));

    // 資料庫連線資訊載入
    DatabaseFullName = Definition.GetText("Property[@Name='Param']/Application/Param[@Name='db_url']");
    DMLUserName = Definition.GetText("Property[@Name='Param']/Application/Param[@Name='db_user']");
    // ... 其他標準參數

    // 載入所有自定參數
    foreach (XmlElement param in Definition.GetElements("Property[@Name='Param']/Application/Param")) {
        string name = param.GetAttribute("Name");
        string val = param.InnerText;
        parameters[name] = val;
    }
}
```

### 5.2 XML 結構範例

```xml
<Application Name="school001">
    <Property Name="Enabled">true</Property>
    <Property Name="Param">
        <Application>
            <Param Name="db_url">Server=localhost;Database=school001;...</Param>
            <Param Name="db_user">school001_user</Param>
            <Param Name="db_pwd">encrypted_password</Param>
            <Param Name="school_code">001</Param>
            <Param Name="app_comment">測試學校系統</Param>
            <Param Name="custom_param">custom_value</Param>
        </Application>
    </Property>
</Application>
```

## 📊 資料庫名稱解析

### 6.1 DatabaseName 屬性邏輯

```csharp
public string DatabaseName {
    get {
        Regex rx = new Regex(Database.ParserPattern);
        return rx.Replace(DatabaseFullName, "${db}");
    }
}
```

**解析目的**: 從完整連線字串中提取純資料庫名稱
- `DatabaseFullName`: `"Server=localhost;Database=school001;User=..."`
- `DatabaseName`: `"school001"`

## 🎭 事件系統分析

### 7.1 應用程式事件

```csharp
// 應用程式層級事件
public event EventHandler NameChanged;      // 名稱變更
public event EventHandler ConfigChanged;    // 配置變更

// 伺服器層級事件
public event EventHandler<AppChangeEventArgs> ApplicationAdded;    // 應用程式新增
public event EventHandler<AppChangeEventArgs> ApplicationRemoved;  // 應用程式移除
```

### 7.2 事件觸發時機

| 事件 | 觸發時機 | 用途 |
|------|---------|------|
| `NameChanged` | 應用程式重新命名後 | 更新 UI 顯示名稱 |
| `ConfigChanged` | 參數或啟用狀態變更後 | 更新 UI 配置顯示 |
| `ApplicationAdded` | 新增應用程式完成後 | 在 UI 中加入新項目 |
| `ApplicationRemoved` | 刪除應用程式完成後 | 從 UI 中移除項目 |


---
📝 **分析完成**: 應用程式生命週期管理邏輯已完整分析，包含所有 CRUD 操作和事件機制