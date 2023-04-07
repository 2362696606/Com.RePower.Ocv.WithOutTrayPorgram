using Com.RePower.Ocv.Model.Entity;
using Com.RePower.Ocv.Model.Enums;

namespace Com.RePower.Ocv.Model.Extensions
{
    public static class NgInfoExtensions
    {
        ///// <summary>
        ///// 设置NgType
        ///// </summary>
        ///// <param name="ngInfo"></param>
        ///// <param name="ngType"></param>
        ///// <param name="value"></param>
        //public static void SetNgType(this NgInfo ngInfo,NgTypeEnum ngType,bool value)
        //{
        //    //BitVector32 bitValue = new BitVector32(ngInfo.NgType ?? 0);
        //    //if (ngType != NgTypeEnum.Normal)
        //    //{
        //    //    int index = (int)ngType;
        //    //    int mask = 0;
        //    //    for (int i = 0; i < index; i++)
        //    //    {
        //    //        mask = BitVector32.CreateMask(mask);
        //    //    }
        //    //    bitValue[mask] = true;
        //    //    ngInfo.NgType = bitValue.Data;
        //    //}
        //    //else
        //    //{
        //    //    ngInfo.NgType = 0;
        //    //}
        //    //var bits = (ngInfo.NgType??0).GetBitList();
        //    ushort index = (ushort)ngType;
        //    if (ngType != NgTypeEnum.Normal)
        //    {
        //        (ngInfo.NgType ?? 0).SetBitValue(index, value);
        //    }
        //    else
        //    {
        //        ngInfo.NgType = 0;
        //    }

        //}
        ///// <summary>
        ///// 根据NgType设置NgDescription
        ///// </summary>
        ///// <param name="ngInfo"></param>
        //public static void SetNgDescriptionByNgType(this NgInfo ngInfo)
        //{
        //    //BitVector32 bitValue = new BitVector32(ngInfo.NgType ?? 0);
        //    //SortedSet<string> ngDescriptions = new SortedSet<string>();
        //    //int mask = 0;
        //    //for(int i=0;i<32;i++)
        //    //{
        //    //    mask = BitVector32.CreateMask(mask);
        //    //    if (bitValue[mask])
        //    //    {
        //    //        if(Enum.TryParse(i.ToString(),out NgTypeEnum currentType) && Enum.IsDefined(currentType))
        //    //        {
        //    //            string currentDescription = currentType.GetDescriptionOriginal();
        //    //            ngDescriptions.Add(currentDescription);
        //    //        }
        //    //        else
        //    //        {
        //    //            ngDescriptions.Add("未知Ng类型");
        //    //        }
        //    //    }
        //    //}
        //    //ngInfo.NgDescription = String.Join('|', ngDescriptions);
        //    SortedSet<string> ngDescriptions = new SortedSet<string>();
        //    var bits = (ngInfo.NgType??0).GetBitList().ToList();
        //    for (int i = 0; i < bits.Count; i++)
        //    {
        //        if (bits[i])
        //        {
        //            if (Enum.TryParse(i.ToString(), out NgTypeEnum currentType) && Enum.IsDefined(currentType))
        //            {
        //                string currentDescription = currentType.GetDescriptionOriginal();
        //                ngDescriptions.Add(currentDescription);
        //            }
        //            else
        //            {
        //                ngDescriptions.Add("未知Ng类型");
        //            }
        //        }
        //    }
        //    ngInfo.NgDescription = String.Join('|', ngDescriptions);
        //}
        ///// <summary>
        ///// 获取所有Ng
        ///// </summary>
        ///// <param name="ngInfo"></param>
        ///// <returns></returns>
        //public static List<NgTypeEnum> GetNgTypes(this NgInfo ngInfo)
        //{
        //    //BitVector32 bitValue = new BitVector32(ngInfo.NgType ?? 0);
        //    //List<NgTypeEnum> ngTypes = new List<NgTypeEnum>();
        //    //int mask = 0;
        //    //for (int i = 0; i < 32; i++)
        //    //{
        //    //    mask = BitVector32.CreateMask(mask);
        //    //    if (bitValue[mask])
        //    //    {
        //    //        if (Enum.TryParse(i.ToString(), out NgTypeEnum currentType))
        //    //        {
        //    //            ngTypes.Add(currentType);
        //    //        }
        //    //    }
        //    //}
        //    //return ngTypes;
        //    var bits = (ngInfo.NgType ?? 0).GetBitList().ToList();
        //    List<NgTypeEnum> ngTypes = new List<NgTypeEnum>();
        //    for(int i = 0;i<bits.Count;i++)
        //    {
        //        if (bits[i])
        //        {
        //            if (Enum.TryParse(i.ToString(), out NgTypeEnum currentType))
        //            {
        //                ngTypes.Add(currentType);
        //            }
        //        }
        //    }
        //    return ngTypes;
        //}
        //public static void SetIsNg(this NgInfo ngInfo)
        //{
        //    if(ngInfo.NgType == 0 || ngInfo.NgType == 1)
        //    {
        //        ngInfo.IsNg = false;
        //    }
        //    else
        //    {
        //        ngInfo.IsNg = true;
        //    }
        //}
        /// <summary>
        /// 判断是否具有某种ng
        /// </summary>
        /// <param name="ngInfo"></param>
        /// <param name="ngType"></param>
        /// <returns></returns>
        public static bool HasNgType(this NgInfo ngInfo, NgTypeEnum ngType)
        {
            bool has = ((((NgTypeEnum)(ngInfo.NgType ?? 0)) & ngType) != 0);
            // bool has = (borderTypeToTest & borderType) == borderType;//这种方法也可以
            return has;
        }

        public static void RemoveNgType(this NgInfo ngInfo, NgTypeEnum ngType)
        {
            bool has = ngInfo.HasNgType(ngType);
            NgTypeEnum currentNgType = (NgTypeEnum)(ngInfo.NgType ?? 0);
            if (has)//必须先判断是否存在
            {
                currentNgType ^= ngType;
                // borderTypeToRemove -= borderType;//这种方法也可以
            }
            ngInfo.NgType = (int)currentNgType;
        }

        /// <summary>
        /// 添加Ng
        /// </summary>
        /// <param name="ngInfo"></param>
        /// <param name="ngType"></param>
        public static void AddNgType(this NgInfo ngInfo, NgTypeEnum ngType)
        {
            NgTypeEnum currentNgType = (NgTypeEnum)(ngInfo.NgType ?? 0);
            currentNgType = currentNgType | ngType;
            ngInfo.NgType = (int)currentNgType;
        }

        ///// <summary>
        ///// 设置Ng描述
        ///// </summary>
        ///// <param name="ngInfo"></param>
        //public static void SetNgDescritpion(this NgInfo ngInfo)
        //{
        //    NgTypeEnum currentNgType = (NgTypeEnum)(ngInfo.NgType ?? 0);
        //    if (ngInfo.NgType == null || ngInfo.NgType == 0 || ngInfo.HasNgType(NgTypeEnum.正常))
        //    {
        //        ngInfo.NgDescription = string.Empty;
        //    }
        //    else
        //    {
        //        ngInfo.NgDescription = currentNgType.ToString();
        //    }
        //}
        //public static string? GetNgDescription(this NgInfo ngInfo)
        //{
        //    NgTypeEnum currentNgType = (NgTypeEnum)(ngInfo.NgType ?? 0);
        //    if (ngInfo.NgType == null || ngInfo.NgType == 0 || ngInfo.HasNgType(NgTypeEnum.正常))
        //    {
        //        return string.Empty;
        //    }
        //    else
        //    {
        //        return currentNgType.ToString();
        //    }
        //}
        ///// <summary>
        ///// 设置是否ng
        ///// </summary>
        ///// <param name="ngInfo"></param>
        //public static void SetIsNg(this NgInfo ngInfo)
        //{
        //    NgTypeEnum currentNgType = (NgTypeEnum)(ngInfo.NgType ?? 0);
        //    if (ngInfo.NgType == null || ngInfo.NgType == 0 || ngInfo.HasNgType(NgTypeEnum.正常))
        //    {
        //        ngInfo.IsNg = false;
        //    }
        //    else
        //    {
        //        ngInfo.IsNg = true;
        //    }
        //}
        //public static bool GetIsNg(this NgInfo ngInfo)
        //{
        //    NgTypeEnum currentNgType = (NgTypeEnum)(ngInfo.NgType ?? 0);
        //    if (ngInfo.NgType == null || ngInfo.NgType == 0 || ngInfo.HasNgType(NgTypeEnum.正常))
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        return true;
        //    }
        //}
    }
}