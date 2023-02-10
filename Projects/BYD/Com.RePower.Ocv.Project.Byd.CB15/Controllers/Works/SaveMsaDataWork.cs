using Com.RePower.Ocv.Project.Byd.CB15.Services.Excel;
using Com.RePower.Ocv.Project.Byd.CB15.Services.Mes.Dtos;
using Com.RePower.WpfBase;
using CsvHelper;
using Npoi.Mapper;
using NPOI.HSSF.Record;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Byd.CB15.Controllers.Works
{
    public partial class MainWork
    {
        /// <summary>
        /// 保存结果到excel
        /// </summary>
        /// <param name="msaTimes"></param>
        /// <returns></returns>
        private OperateResult SaveMsaData(int msaTimes)
        {
            string dir = @$"./数据文件夹_msa/{Tray.TrayCode}";
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            string path = @$"./数据文件夹_msa/{Tray.TrayCode}/{Tray.TrayCode}_{msaTimes}.csv";
            switch (SettingManager.CurrentOcvType)
            {
                case Enums.OcvTypeEnmu.OCV1:
                    return SaveTestDateToCsv<Ocv1InfoDto>(path);
                case Enums.OcvTypeEnmu.OCV2:
                    return SaveTestDateToCsv<Ocv2InfoDto>(path);
                case Enums.OcvTypeEnmu.OCV3:
                    return SaveTestDateToCsv<Ocv3InfoDto>(path);
                case Enums.OcvTypeEnmu.OCV4:
                    return SaveTestDateToCsv<Ocv4InfoDto>(path);
                default:
                    return SaveTestDateToCsv<Ocv1InfoDto>(path);
            }
        }

        private OperateResult SaveMsaDateToExcel<T>(int msaTimes)
        {
            List<T> saveDtos = new List<T>();
            foreach (var item in Tray.NgInfos)
            {
                var temp = Mapper.Map<T>(item);
                saveDtos.Add(temp);
            }
            try
            {
                string dir = @$"./正常数据文件夹_msa/{Tray.TrayCode}";
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                new Mapper().Save(@$"./正常数据文件夹_msa/{Tray.TrayCode}/{Tray.TrayCode}_{msaTimes}.xlsx", saveDtos, "sheet1");
                return OperateResult.CreateSuccessResult();
            }
            catch (Exception e)
            {
                return OperateResult.CreateFailedResult($"保存失败：{e.Message}");
                throw;
            }
        }

        private OperateResult SaveTestDateToCsv<T>(string path)
        {
            List<T> dtos = Mapper.Map<List<T>>(Tray.NgInfos);
            try
            {
                using (var writer = new StreamWriter(path))
                {
                    var csvConfig = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture);
                    using (var csv = new CsvWriter(writer, csvConfig))
                    {
                        csv.WriteRecords(dtos);
                    }
                }
                return OperateResult.CreateSuccessResult();
            }
            catch (Exception e)
            {
                return OperateResult.CreateFailedResult($"MSA保存到CSV出错：{e.Message}");
            }
        }

        private OperateResult Ocv4SaveMsaDateToExcel(int msaTimes)
        {
            List<Ocv4InfoDto> saveDtos = new List<Ocv4InfoDto>();
            foreach(var item in Tray.NgInfos)
            {
                var temp = Mapper.Map<Ocv4InfoDto>(item);
                saveDtos.Add(temp);
            }
            try
            {
                string dir = @$"./正常数据文件夹_msa/{Tray.TrayCode}";
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                new Mapper().Save(@$"./正常数据文件夹_msa/{Tray.TrayCode}/{Tray.TrayCode}_{msaTimes}.xlsx", saveDtos, "sheet1");
                return OperateResult.CreateSuccessResult();
            }
            catch (Exception e)
            {
                return OperateResult.CreateFailedResult($"保存失败：{e.Message}");
                throw;
            }
        }

        private OperateResult Ocv3SaveMsaDateToExcel(int msaTimes)
        {
            List<Ocv3InfoDto> saveDtos = new List<Ocv3InfoDto>();
            foreach (var item in Tray.NgInfos)
            {
                var temp = Mapper.Map<Ocv3InfoDto>(item);
                saveDtos.Add(temp);
            }
            try
            {
                string dir = @$"./正常数据文件夹_msa/{Tray.TrayCode}";
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                new Mapper().Save(@$"./正常数据文件夹_msa/{Tray.TrayCode}/{Tray.TrayCode}_{msaTimes}.xlsx", saveDtos, "sheet1");
                return OperateResult.CreateSuccessResult();
            }
            catch (Exception e)
            {
                return OperateResult.CreateFailedResult($"保存失败：{e.Message}");
                throw;
            }
        }

        private OperateResult Ocv2SaveMsaDateToExcel(int msaTimes)
        {
            List<Ocv2InfoDto> saveDtos = new List<Ocv2InfoDto>();
            foreach (var item in Tray.NgInfos)
            {
                var temp = Mapper.Map<Ocv2InfoDto>(item);
                saveDtos.Add(temp);
            }
            try
            {
                string dir = @$"./正常数据文件夹_msa/{Tray.TrayCode}";
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                new Mapper().Save(@$"./正常数据文件夹_msa/{Tray.TrayCode}/{Tray.TrayCode}_{msaTimes}.xlsx", saveDtos, "sheet1");
                return OperateResult.CreateSuccessResult();
            }
            catch (Exception e)
            {
                return OperateResult.CreateFailedResult($"保存失败：{e.Message}");
                throw;
            }
        }

        private OperateResult Ocv1SaveMsaDateToExcel(int msaTimes)
        {
            List<Ocv1InfoDto> saveDtos = new List<Ocv1InfoDto>();
            foreach (var item in Tray.NgInfos)
            {
                var temp = Mapper.Map<Ocv1InfoDto>(item);
                saveDtos.Add(temp);
            }
            try
            {
                string dir = @$"./正常数据文件夹_msa/{Tray.TrayCode}";
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                new Mapper().Save(@$"./正常数据文件夹_msa/{Tray.TrayCode}/{Tray.TrayCode}_{msaTimes}.xlsx", saveDtos, "sheet1");
                return OperateResult.CreateSuccessResult();
            }
            catch (Exception e)
            {
                return OperateResult.CreateFailedResult($"保存失败：{e.Message}");
                throw;
            }
        }
    }
}
