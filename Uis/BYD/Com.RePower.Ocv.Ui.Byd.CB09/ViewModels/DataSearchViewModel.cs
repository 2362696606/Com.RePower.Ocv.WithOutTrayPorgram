using Com.RePower.Ocv.Model.DataBaseContext;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Ui.Byd.CB09.ViewModels
{
    public class DataSearchViewModel:ObservableObject
    {
        public DataSearchViewModel(LocalTestResultDbContext dbContext)
        {
            _dataContext = new UiBase.ViewModels.DataSearchViewModel(dbContext);
        }

        private object _dataContext;

        public object DataContext
        {
            get { return _dataContext; }
            set { SetProperty(ref _dataContext, value); }
        }
    }
}
