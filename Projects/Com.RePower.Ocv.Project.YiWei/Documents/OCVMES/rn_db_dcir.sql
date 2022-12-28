/*
 Navicat Premium Data Transfer

 Source Server         : 172.17.20.35
 Source Server Type    : SQL Server
 Source Server Version : 15002000
 Source Host           : 172.17.20.35:1433
 Source Catalog        : WuWei_Byd_Ocv_04
 Source Schema         : dbo

 Target Server Type    : SQL Server
 Target Server Version : 15002000
 File Encoding         : 65001

 Date: 07/11/2022 15:50:51
*/


-- ----------------------------
-- Table structure for rn_db_dcir
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[rn_db_dcir]') AND type IN ('U'))
	DROP TABLE [dbo].[rn_db_dcir]
GO

CREATE TABLE [dbo].[rn_db_dcir] (
  [ID] bigint  IDENTITY(1,1) NOT NULL,
  [Eqp_ID] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [PC_ID] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [OPERATION] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [IS_TRANS] decimal(1)  NULL,
  [TRAY_ID] varchar(50) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [CELL_ID] varchar(50) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [BATTERY_POS] int  NULL,
  [MODEL_NO] varchar(36) COLLATE Chinese_PRC_CI_AS  NULL,
  [OpenVoltage] decimal(8,4)  NULL,
  [D_DCIR] decimal(8,2)  NULL,
  [C_DCIR] decimal(8,2)  NULL,
  [TC_D_DCIR] decimal(8,2)  NULL,
  [TC_C_DCIR] decimal(8,2)  NULL,
  [D_EndVoltage] decimal(8,4)  NULL,
  [C_EndVoltage] decimal(8,4)  NULL,
  [D_EndCurrent] decimal(8,4)  NULL,
  [C_EndCurrent] decimal(8,4)  NULL,
  [D_DCIR_Delta] decimal(8,4)  NULL,
  [NG_CODE] varchar(8) COLLATE Chinese_PRC_CI_AS  NULL,
  [AnodeTemp] decimal(8,2)  NULL,
  [CathodeTemp] decimal(8,2)  NULL,
  [Max_AnodeTemp] decimal(8,2)  NULL,
  [Max_CathodeTemp] decimal(8,2)  NULL,
  [END_DATE_TIME] datetime  NULL
)
GO

ALTER TABLE [dbo].[rn_db_dcir] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Auto increment value for rn_db_dcir
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[rn_db_dcir]', RESEED, 225922)
GO


-- ----------------------------
-- Primary Key structure for table rn_db_dcir
-- ----------------------------
ALTER TABLE [dbo].[rn_db_dcir] ADD CONSTRAINT [PK__rn_db_dc__3214EC274B16DA18] PRIMARY KEY CLUSTERED ([ID])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO

