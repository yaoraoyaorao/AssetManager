# AssetManager

## 数据库分析

### 基础
主键:Id long
创建时间:CreateTime DateTime
更新时间:UpdateTime DateTime

### 平台(Platform)
平台名:Name string (不能重复)
图标:Icon string
备注:Remark string

### 平台资源(PlatformAsset)
目标平台:TargetPlatform Platform (不能重复)
资源路径:AssetPath string
目标资源包:TargetAssetPackage AssetPackage

### 资源包(AssetPackage)
大版本:Max int (不能重复)
小版本:Min int (不能重复)
补丁版本:Pitch int (不能重复)
审核状态:AuditStatus int
平台资源列表:PlatformRAssets List\<PlatformRAsset\>
目标项目:TargetProject Project

### 项目(Project)
项目名称:Name string (不能重复)
项目简介:Description string
项目GUID:Guid string
资源包：ResourcePackages List\<AssetPackage\>
