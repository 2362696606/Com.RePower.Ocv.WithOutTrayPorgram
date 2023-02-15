using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.CZD01.BaseProject.Settings.Dtos
{
    public class TestOptionDto:Ocv.Model.Settings.Dtos.TestOptionDto
    {
        /// <summary>
        /// 是否验证压差
        /// </summary>
        public bool IsVerifyVolDifference { get; set; }
        /// <summary>
        /// 是否验证单托盘k值Ng
        /// </summary>
        public bool IsVerifyCurrentKValue { get; set; }
    }
}
