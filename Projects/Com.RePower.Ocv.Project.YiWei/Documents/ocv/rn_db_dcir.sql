/*
 Navicat Premium Data Transfer

 Source Server         : OcvData
 Source Server Type    : SQLite
 Source Server Version : 3035005 (3.35.5)
 Source Schema         : main

 Target Server Type    : SQLite
 Target Server Version : 3035005 (3.35.5)
 File Encoding         : 65001

 Date: 16/12/2022 13:53:08
*/

PRAGMA foreign_keys = false;

-- ----------------------------
-- Table structure for rn_db_dcir
-- ----------------------------
DROP TABLE IF EXISTS "rn_db_dcir";
CREATE TABLE "rn_db_dcir" (
  "ID" TEXT(255),
  "Eqp_ID" TEXT(255),
  "PC_ID" TEXT(255),
  "OPERATION" TEXT(255),
  "IS_TRANS" TEXT(255),
  "TRAY_ID" TEXT(255),
  "CELL_ID" TEXT(255),
  "BATTERY_POS" TEXT(255),
  "MODEL_NO" TEXT(255),
  "OpenVoltage" TEXT(255),
  "D_DCIR" TEXT(255),
  "C_DCIR" TEXT(255),
  "TC_D_DCIR" TEXT(255),
  "TC_C_DCIR" TEXT(255),
  "D_EndVoltage" TEXT(255),
  "C_EndVoltage" TEXT(255),
  "D_EndCurrent" TEXT(255),
  "C_EndCurrent" TEXT(255),
  "D_DCIR_Delta" TEXT(255),
  "NG_CODE" TEXT(255),
  "AnodeTemp" TEXT(255),
  "CathodeTemp" TEXT(255),
  "Max_AnodeTemp" TEXT(255),
  "Max_CathodeTemp" TEXT(255),
  "END_DATE_TIME" TEXT(255)
);

PRAGMA foreign_keys = true;
