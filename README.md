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
平台资源列表:PlatformAssets List\<PlatformRAsset\>
目标项目:TargetProject Project

### 项目(Project)
项目名称:Name string (不能重复)
项目简介:Description string
项目GUID:Guid string
资源包：ResourcePackages List\<AssetPackage\>

## 接口设计
### 项目接口
#### 添加项目
**接口：api/Project/Add**
**方法：Post** 
**参数:**
|参数名|类型|描述|是否必需|示例值|
|-----|----|----|-------|-----|
|Id   |long|项目ID|否|123|
|Name |string|项目名称|是|"Project"|
|Description|string|项目描述|否|"Description of Project"|

**请求示例**
```json
{
  "id": 0,
  "name": "项目名",
  "description": "项目描述"
}
```
**响应示例**
```json
{
  "code": 200,
  "message": "添加成功",
  "data": {
    "name": "string",
    "description": "string",
    "guid": "ae830c20-82bc-4673-be31-c3a21b033ce0",
    "assetPackages": [
      {
        "auditStatus": 0,
        "max": 1,
        "min": 0,
        "patch": 0,
        "platformAssets": [],
        "id": 9
      }
    ],
    "id": 10
  }
}
```
code为200表示成功 400表示失败

#### 更新项目
**接口：api/Project/Update**
**方法：Post** 
**参数:**
|参数名|类型|描述|是否必需|示例值|
|-----|----|----|-------|-----|
|Id   |long|项目ID|是|123|
|Name |string|项目名称|是|"Project"|
|Description|string|项目描述|否|"Description of Project"|

**请求示例**
```json
{
  "id": 0,
  "name": "项目名",
  "description": "项目描述"
}
```
**响应示例**
```json
{
  "code": 200,
  "message": "更新成功",
  "data": {
    "name": "测试项目123",
    "description": "项目描述1234",
    "guid": "ae830c20-82bc-4673-be31-c3a21b033ce0",
    "assetPackages": [],
    "id": 10
  }
}
```










