# 資料庫操作業務邏輯分析

## 📋 概述

資料庫操作是 DSA Server 管理系統的核心功能之一，提供 PostgreSQL 資料庫的管理、查詢、更新和監控功能。系統透過 FISCA DSA 協議與資料庫服務器通訊，支援多資料庫管理和權限控制。

## 🏗️ 核心類別分析

### 1. DatabaseManager 類別 (`Manager/Model/DatabaseManager.cs`)

**職責**: 封裝所有資料庫相關操作，提供統一的資料庫管理介面

```csharp
class DatabaseManager {
    private ServerManager Manager { get; set; }    // 伺服器管理器
    public string TargetDatabase { get; set; }     // 目標資料庫名稱

    // 核心操作方法
    public XmlElement ExecuteUpdate(string cmd)     // 執行更新命令
    public XmlElement ExecuteQuery(string cmd)      // 執行查詢命令

    // 資料庫資訊查詢
    public string GetDBMSVersionString()            // 取得資料庫版本
    public string GetTargetDatabase()               // 取得當前資料庫名稱
    public long GetDatabaseSize()                   // 取得資料庫大小
    public long GetTableSize(string tableName)     // 取得資料表大小

    // 結構查詢
    public List<string> ListTables()                // 列出所有資料表
    public List<string> ListViews()                 // 列出所有檢視表
    public List<string> ListSequences()             // 列出所有序列
    public List<string> ListTrigger()               // 列出所有觸發器
    public List<RoleData> ListRoles()              // 列出所有角色
}
```

### 2. ServerManager 中的資料庫方法

**職責**: 處理資料庫連線層級的操作

```csharp
class ServerManager {
    // 資料庫列表管理
    internal List<Database> ListDatabases()                    // 列出所有資料庫
    internal bool DatabaseExists(string dbName)               // 檢查資料庫是否存在
    internal void CreateNewDatabase(string dbName, string templateDB)  // 建立新資料庫

    // 連線測試
    internal Exception TestAccount(AccountData account)        // 測試帳號連線
    internal XmlElement TestConnection(string appName)        // 測試應用程式連線
    internal XmlElement TestConnection()                      // 測試預設連線

    // 資料庫操作
    internal XmlElement ExecuteQueryCommand(string cmd, string database)
    internal XmlElement ExecuteUpdateCommand(string cmd, string database)
    internal XmlElement ExecuteUpdateCommands(List<string> cmds, string database)

    // 特殊操作
    internal void TerminalDBConnection(string dbName)         // 終止資料庫連線
    internal XmlElement ResetLicenseCode(string dbName, string code)  // 重置授權碼
}
```

### 3. Database 模型類別 (`Manager/Model/Database.cs`)

**職責**: 代表一個資料庫實例的基本資訊

```csharp
class Database {
    public string OID { get; set; }         // PostgreSQL 物件識別碼
    public string Name { get; set; }        // 資料庫名稱
    public string Description { get; set; } // 資料庫描述

    // 連線字串解析模式
    public const string ParserPattern = @"(?<pgstring>.*Database=)(?<db>[^;]+)(?<remain>;?.*)";
}
```

## 🗄️ 資料庫查詢操作分析

### 4.1 基本查詢操作

**資料庫版本查詢**:
```sql
-- DatabaseManager.GetDBMSVersionString()
SELECT version();
```

**當前資料庫查詢**:
```sql
-- DatabaseManager.GetTargetDatabase()
SELECT current_database();
```

**資料庫大小查詢**:
```sql
-- DatabaseManager.GetDatabaseSize()
SELECT pg_database_size('{資料庫名稱}');
```

**資料表大小查詢**:
```sql
-- DatabaseManager.GetTableSize()
SELECT (pg_total_relation_size('{資料表名稱}'::regclass)) as tablesize;
```

### 4.2 結構資訊查詢

**列出資料表**:
```sql
-- DatabaseManager.ListTables()
SELECT table_name
FROM information_schema.tables
WHERE table_schema='public';
```

**列出檢視表**:
```sql
-- DatabaseManager.ListViews()
SELECT table_name
FROM information_schema.views
WHERE table_schema='public';
```

**列出序列**:
```sql
-- DatabaseManager.ListSequences()
SELECT sequence_name
FROM information_schema.sequences
WHERE sequence_schema='public';
```

**列出觸發器**:
```sql
-- DatabaseManager.ListTrigger()
SELECT routine_name
FROM information_schema.routines
WHERE routine_schema='public' AND data_type='trigger';
```

**列出角色**:
```sql
-- DatabaseManager.ListRoles()
SELECT rolname, rolcanlogin
FROM pg_roles
ORDER BY rolcanlogin, rolname;
```

### 4.3 資料庫管理查詢

**列出所有資料庫** (透過 SQL 常數):
```sql
-- ServerManager.ListDatabases() 使用 SQL.ListDatabase
-- 具體 SQL 內容在 SQL.Designer.cs 中定義
```

**建立新資料庫**:
```sql
-- ServerManager.CreateNewDatabase() 使用 SQL.NewDatabase
-- 格式: CREATE DATABASE {新資料庫名稱} WITH TEMPLATE {樣板資料庫名稱};
```

## 🔐 權限與安全機制

### 5.1 帳號權限體系

**超級使用者機制**:
```csharp
// 檢查是否使用超級使用者帳號
if (Srv.SuperUser != AccountData.Default) {
    req.SetAttribute("Command", "UserName", Srv.SuperUser.UserName);
    req.SetAttribute("Command", "Password", Srv.SuperUser.Password);
}
```

**雙重帳號設計**:
- **DML User**: 負責資料的增刪改查操作
- **DDL User**: 負責資料庫結構的修改操作

**AccountData 結構**:
```csharp
class AccountData {
    public string UserName { get; set; }
    public string Password { get; set; }
    public static AccountData Default { get; }  // 預設帳號常數
}
```

### 5.2 連線測試機制

**基本連線測試**:
```csharp
internal Exception TestAccount(AccountData account) {
    try {
        FISCA.XHelper req = new FISCA.XHelper();
        req.AddElement(".", "Command", "select now();");

        if (Srv.SuperUser != AccountData.Default) {
            req.SetAttribute("Command", "UserName", account.UserName);
            req.SetAttribute("Command", "Password", account.Password);
        }

        Connection.SendRequest("Database.Query", new Envelope(req));
        return null;  // 成功
    } catch (Exception ex) {
        return ex;    // 回傳錯誤
    }
}
```

## 📡 FISCA 資料庫服務分析

### 6.1 核心 FISCA 服務

| FISCA 服務名稱 | 功能 | 說明 |
|---------------|------|------|
| `Database.Query` | 執行 SELECT 查詢 | 回傳 XML 格式的查詢結果 |
| `Database.Command` | 執行 INSERT/UPDATE/DELETE | 回傳執行狀態 |
| `Database.Commands` | 批次執行多個命令 | 支援交易處理 |

### 6.2 請求格式分析

**單一查詢請求**:
```xml
<Request>
    <Command Database="目標資料庫" UserName="使用者" Password="密碼">
        SELECT * FROM table_name;
    </Command>
</Request>
```

**批次命令請求**:
```xml
<Request>
    <Commands Database="目標資料庫" UserName="使用者" Password="密碼">
        <Command>INSERT INTO table1 VALUES (...);</Command>
        <Command>UPDATE table2 SET ...;</Command>
        <Command>DELETE FROM table3 WHERE ...;</Command>
    </Commands>
</Request>
```

### 6.3 回應格式分析

**查詢結果格式**:
```xml
<Response>
    <Record>
        <column1>value1</column1>
        <column2>value2</column2>
    </Record>
    <Record>
        <column1>value3</column1>
        <column2>value4</column2>
    </Record>
</Response>
```

## 🛠️ SQL 命令處理機制

### 7.1 SQL 分割邏輯

```csharp
private List<string> SplitCommand(string command) {
    List<StringBuilder> commands = new List<StringBuilder>();
    Regex spliter = new Regex(@";\s*$", RegexOptions.Compiled);  // 分號結尾

    StringReader cmdReader = new StringReader(command);
    StringBuilder singleCmd = new StringBuilder();

    while (cmdReader.Peek() >= 0) {
        string matchSource = cmdReader.ReadLine();
        if (string.IsNullOrWhiteSpace(matchSource)) continue;

        singleCmd.AppendLine(matchSource);

        if (spliter.Match(matchSource).Success) {  // 遇到分號結尾
            singleCmd = new StringBuilder();        // 開始新命令
            commands.Add(singleCmd);
        }
    }

    return commands.Where(cmd => !string.IsNullOrWhiteSpace(cmd.ToString())).ToList();
}
```

### 7.2 批次命令處理

```csharp
internal XmlElement ExecuteUpdateCommands(List<string> cmds, string database) {
    if (cmds.Count == 1)
        return ExecuteUpdateCommand(cmds[0], database);

    Version statableVer = new Version(4, 1, 7, 1);  // 穩定版本檢查
    Version curVer = GetCurrentVersion();

    if (curVer >= statableVer)
        return UseCommandsService(cmds, database);   // 使用批次服務
    else
        return SaveStatementsToLocal(cmds, database); // 舊版本降級處理
}
```

**版本相容性處理**:
- DSA 4.1.7.1 以上版本: 支援 `Database.Commands` 批次服務
- 舊版本: 將 SQL 語句儲存到本地檔案，並拋出異常提示升級

## 🔄 連線管理機制

### 8.1 資料庫連線終止

```csharp
internal void TerminalDBConnection(string dbName) {
    // 查詢所有連線到指定資料庫的 Process ID
    string sql = string.Format("select procpid from pg_stat_activity where datname='{0}'", dbName);
    FISCA.XHelper rsp = new FISCA.XHelper(ExecuteQueryCommand(sql, dbName));

    // 並行終止所有連線
    Parallel.ForEach(rsp.GetElements("Record"), each => {
        string pid = each.SelectSingleNode("procpid").InnerText;
        string termsql = string.Format("select pg_terminate_backend({0});", pid);
        ExecuteQueryCommand(termsql, dbName);
    });
}
```

### 8.2 特殊操作 - 重置授權碼

```csharp
internal XmlElement ResetLicenseCode(string dbName, string code) {
    string sql = string.Format("update sys_security set secure_code ='{0}'", code);
    return ExecuteUpdateCommand(sql, dbName);
}
```


---
📝 **分析完成**: 資料庫操作核心邏輯已完整分析，包含所有 FISCA 協議互動和 PostgreSQL 操作模式