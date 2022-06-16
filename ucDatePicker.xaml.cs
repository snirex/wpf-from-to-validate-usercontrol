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
      if(values.LongLength >0 && values[0] != null && values[1] != null && values[2] != null && values[3] != null)
      {
         DateTime startDate = (DateTime)values[0];
         DateTime endDate = (DateTime)values[1];
         DateTime startTime = (DateTime)values[2]; // telerik time picker
         DateTime endTime = (DateTime)values[3]; // telerik time picker
         
         //Join date + time
         DateTime from = startDate.Add(startTime.TimeOfDay);
         DateTime to = endDate.Add(endTime.TimeOfDay);
         
         return from <= to ? Brushes.Transparent : Brushes.Red;
      }
      return Brushes.Transparent;
    }
    
    pulbic object[] Convertback(object value, Type[] targetType, object parmaeter, CultureIfo culture)
    {
       throw new NotImplementedException();
    }
  }
}
