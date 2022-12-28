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

 Date: 07/11/2022 15:51:12
*/


-- ----------------------------
-- Table structure for rn_db_ocv4
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[rn_db_ocv4]') AND type IN ('U'))
	DROP TABLE [dbo].[rn_db_ocv4]
GO

CREATE TABLE [dbo].[rn_db_ocv4] (
  [ID] bigint  IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
  [Eqp_ID] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [PC_ID] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [OPERATION] varchar(50) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [IS_TRANS] decimal(1)  NOT NULL,
  [TRAY_ID] varchar(50) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [CELL_ID] varchar(50) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [BATTERY_POS] int  NOT NULL,
  [MODEL_NO] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [BATCH_NO] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [TOTAL_NG_STATE] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [OCV_VOLTAGE] decimal(16,7)  NOT NULL,
  [ACIR] decimal(20,7)  NULL,
  [TEST_NG_CODE] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [TEST_RESULT] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [TEST_RESULT_DESC] varchar(150) COLLATE Chinese_PRC_CI_AS  NULL,
  [PostiveSHELL_VOLTAGE] decimal(16,7)  NULL,
  [PostiveSV_NG_CODE] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [PostiveSV_RESULT] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [PostiveSV_RESULT_DESC] varchar(150) COLLATE Chinese_PRC_CI_AS  NULL,
  [SHELL_VOLTAGE] decimal(16,7)  NULL,
  [SV_NG_CODE] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [SV_RESULT] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [SV_RESULT_DESC] varchar(150) COLLATE Chinese_PRC_CI_AS  NULL,
  [POSTIVE_TEMP] decimal(8,1)  NULL,
  [NEGATIVE_TEMP] decimal(8,1)  NULL,
  [K] decimal(16,6)  NULL,
  [V_DROP] decimal(16,7)  NULL,
  [V_DROP_RANGE] decimal(16,7)  NULL,
  [V_DROP_RANGE_CODE] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [V_DROP_RESULT] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [V_DROP_RESULT_DESC] varchar(150) COLLATE Chinese_PRC_CI_AS  NULL,
  [ACIR_RANGE] decimal(20,6)  NULL,
  [R_RANGE_NG_CODE] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [R_RANGE_RESULT] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [R_RANGE_RESULT_DESC] varchar(150) COLLATE Chinese_PRC_CI_AS  NULL,
  [REV_OCV] decimal(16,7)  NULL,
  [CAPACITY] decimal(16,7)  NULL,
  [END_DATE_TIME] datetime  NOT NULL,
  [InsertTime] datetime  NULL,
  [TestMode] varchar(50) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [DischargeTime] decimal(18,2)  NULL
)
GO

ALTER TABLE [dbo].[rn_db_ocv4] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Auto increment value for rn_db_ocv4
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[rn_db_ocv4]', RESEED, 2020920)
GO


-- ----------------------------
-- Primary Key structure for table rn_db_ocv4
-- ----------------------------
ALTER TABLE [dbo].[rn_db_ocv4] ADD CONSTRAINT [PK_rn_db_ocv4] PRIMARY KEY CLUSTERED ([ID])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO

