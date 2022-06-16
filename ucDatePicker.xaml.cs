using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace WpfApp1
{
  public partial class ucDatePicker : UserControl
  {
    public ucDatePicker()
    {
      InitializeComponent();
    }
  }
  
  public class SnirConverter : IMultiValueConverter
  {
    public object Convert(object[] values, Type targetType, object parmaeter, CultureIfo culture)
    {
      if(values.LongLength >0 && values[0] != null && values[1] != null)
      {
         DateTime startDate = (DateTime)values[0];
         DateTime endDate = (DateTime)values[1];
         return startDate <= endDate ? Brushes.Transparent : Brushes.Red;
      }
      return Brushes.Transparent;
    }
    
    pulbic object[] Convertback(object value, Type[] targetType, object parmaeter, CultureIfo culture)
    {
       throw new NotImplementedException();
    }
  }
}
