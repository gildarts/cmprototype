# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## 專案概述

CloudManager_Prototype 是一個 DSA (Digital Student Archive) Server 管理工具，使用 .NET Framework 4.8 和 Windows Forms 開發。主要功能是管理和監控 DSA 伺服器系統，提供資料庫管理、應用程式部署、日誌查看等功能。

## 建置與開發

### 必要環境
- .NET Framework 4.8
- Visual Studio 2013 或更新版本
- Windows 作業系統

### 建置指令
```bash
# 建置專案
msbuild CloudManager_Prototype.sln /p:Configuration=Debug /p:Platform="Mixed Platforms"

# Release 建置
msbuild CloudManager_Prototype.sln /p:Configuration=Release /p:Platform="Mixed Platforms"

# 清理專案
msbuild CloudManager_Prototype.sln /t:Clean
```

### 專案結構

#### 核心專案
- **Manager**: 主要的 WinForms 應用程式專案 (WinExe, x86)
- **Manager.Interfaces**: 共享介面定義的函式庫專案 (Library, AnyCPU)
- **Library/**: 包含所有第三方 DLL 相依性

#### 主要架構模式
- **WinForms + DevComponents**: 使用 DevComponents.DotNetBar2 作為 UI 框架
- **Model-View 架構**: 核心邏輯在 `Manager/Model/` 目錄下
- **FISCA DSA 整合**: 透過 FISCA.Core 和 FISCA.DSAClient 進行伺服器通訊
- **Plugin 架構**: 支援透過 IAppUpgrader 介面進行應用程式升級擴展

#### 核心元件

**伺服器管理 (Manager/Model/)**
- `ServerManager.cs`: 伺服器管理核心邏輯
- `ServerCollection.cs`: 伺服器集合管理
- `Server.cs`: 個別伺服器物件模型
- `ServerConfiguration.cs`: 伺服器組態設定
- `Application.cs`: 應用程式部署管理

**資料庫管理**
- `DatabaseManager.cs`: 資料庫連線和管理
- `Database.cs`: 資料庫物件模型

**日誌系統**
- `LogConfiguration.cs`: 日誌組態設定
- `LogQuery.cs`: 日誌查詢功能
- `ExceptionReport.cs`: 例外報告處理

**安全性**
- `AesKey.cs`: AES 加密金鑰管理
- `AccountData.cs`: 帳戶資料處理
- `CloneSecurityToken.cs`: 安全權杖複製

#### 主要 UI 元件

**主視窗**
- `MainForm.cs`: 主要應用程式視窗
- `ServerGroupPanel.cs`: 伺服器群組面板
- `ServerManagePanel.cs`: 伺服器管理面板

**功能視窗**
- `LoginForm.cs`: 登入表單
- `DatabaseListForm.cs`: 資料庫列表
- `LogViewer.cs`: 日誌檢視器
- `DBDebugger.cs`: 資料庫除錯工具
- `XmlEditor.cs`: XML 編輯器

**文字編輯器 (TextEditor/)**
- `BaseSyntaxEditor.cs`: 基礎語法編輯器
- `FindReplaceForm.cs`: 尋找取代功能
- `XPathEvaluatorForm.cs`: XPath 表達式評估器

### 第三方元件相依性

**ActiproSoftware**: 語法編輯器元件
- ActiproSoftware.SyntaxEditor.Net20.dll
- ActiproSoftware.Shared.Net20.dll
- ActiproSoftware.WinUICore.Net20.dll

**DevComponents**: UI 元件庫
- DevComponents.DotNetBar2.dll

**Aspose**: Office 文件處理
- Aspose.Cells.dll (需授權檔案)

**FISCA**: DSA 系統整合
- FISCA.Core.dll
- FISCA.DSAClient.dll
- FISCA.Authentication.dll

### 組態設定
- `app.config`: 應用程式組態檔案
- `configuration.xml`: 自訂組態設定
- License 檔案位於 `Resources/Aspose.Total.lic`

### 安全性設定
- TLS 1.2 連線 (Program.cs:95)
- SSL 憑證驗證已停用 (僅限開發環境)
- AES 加密密碼儲存
- 安全權杖管理機制

### 開發注意事項
- 專案使用 x86 平台建置 (主要專案)
- 相依 FISCA 框架的特定版本
- 需要 Windows 環境執行
- 包含授權元件 (Aspose)，部署時需注意授權問題
- NameService 功能模組支援 DSA 名稱解析
- 支援多工處理和非同步操作 (AsyncRunner, MultiTaskingRunner)

### 升級機制
- 透過 `Update()` 方法自動更新
- Manifest 驗證和數位簽章檢查
- 支援 Plugin 架構透過 IAppUpgrader 介面擴展