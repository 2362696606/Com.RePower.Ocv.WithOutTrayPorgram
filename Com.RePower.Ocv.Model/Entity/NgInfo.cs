using Com.RePower.Ocv.Model.Enums;
using Com.RePower.Ocv.Model.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Com.RePower.Ocv.Model.Entity
{
    public partial class NgInfo : ObservableObject
    {
        private Battery _battery = new Entity.Battery();

        /// <summary>
        /// 对应电池
        /// </summary>
        public Battery Battery
        {
            get { return _battery; }
            set { SetProperty(ref _battery, value); }
        }

        /// <summary>
        /// Ng描述
        /// </summary>
        public string? NgDescription => this.GetNgDescription();

        private bool? _attachedIsNg;

        /// <summary>
        /// 附加Ng，true为ng，false为非ng
        /// </summary>
        public bool? AttachedIsNg
        {
            get { return _attachedIsNg; }
            set
            {
                if (SetProperty(ref _attachedIsNg, value))
                {
                    OnPropertyChanged(nameof(IsNg));
                }
            }
        }

        private string? _attachedNgDescription;

        /// <summary>
        /// 附加Ng描述
        /// </summary>
        public string? AttachedNgDescription
        {
            get { return _attachedNgDescription; }
            set
            {
                if (SetProperty(ref _attachedNgDescription, value))
                {
                    OnPropertyChanged(nameof(NgDescription));
                }
            }
        }

        private int? _ngType;

        /// <summary>
        /// ng类型
        /// </summary>
        public int? NgType
        {
            get { return _ngType; }
            set
            {
                if (SetProperty(ref _ngType, value))
                {
                    OnPropertyChanged(nameof(NgDescription));
                    OnPropertyChanged(nameof(IsNg));
                }
            }
        }

        /// <summary>
        /// 是否ng true为ng，false为非ng
        /// </summary>
        public bool IsNg => this.GetIsNg();

        /// <summary>
        /// 获取ng描述
        /// </summary>
        /// <returns></returns>
        private string? GetNgDescription()
        {
            NgTypeEnum currentNgType = (NgTypeEnum)(this.NgType ?? 0);
            string? tempNgDescription = string.Empty;
            if (this.NgType == null || this.NgType == 0 || this.HasNgType(NgTypeEnum.正常))
            {
                if (!string.IsNullOrEmpty(AttachedNgDescription))
                    tempNgDescription = AttachedNgDescription;
            }
            else
            {
                tempNgDescription = currentNgType.ToString();
                tempNgDescription = string.Join(' ', tempNgDescription, AttachedNgDescription);
            }
            return tempNgDescription;
        }

        /// <summary>
        /// 获取是否ng
        /// </summary>
        /// <returns></returns>
        private bool GetIsNg()
        {
            if (AttachedIsNg ?? false)
                return true;
            NgTypeEnum currentNgType = (NgTypeEnum)(this.NgType ?? 0);
            if (this.NgType == null || this.NgType == 0 || this.HasNgType(NgTypeEnum.正常))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}