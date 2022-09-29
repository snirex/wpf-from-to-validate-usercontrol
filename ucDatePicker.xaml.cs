using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace WpfApp1
{
  public partial class ucDatePicker : UserControl
  {
    #region DependencyProperties
    public DateTime SelectedDateStart
    {
      get { return (DateTime)GetValue(SelectedDateStartProperty); }
      set { SetValue(SelectedDateStartProperty, true); }
    }
    public static readonly DependencyProperty SelectedDateStartProperty =
          DependencyProperty.Register("SelectedDateStart", typeof(DateTime), typeof(ucDatePicker), new PropertyMetaData(DateTime.Now));
          
    public DateTime SelectedDateEnd
    {
      get { return (DateTime)GetValue(SelectedDateEndProperty); }
      set { SetValue(SelectedDateEndProperty, true); }
    }
    public static readonly DependencyProperty SelectedDateEndProperty =
          DependencyProperty.Register("SelectedDateEnd", typeof(DateTime), typeof(ucDatePicker), new PropertyMetaData(DateTime.Now));
          
          
    public DateTime ValueTimeStart
    {
      get { return (DateTime)GetValue(ValueTimeStartProperty); }
      set { SetValue(ValueTimeStartProperty, true); }
    }
    public static readonly DependencyProperty ValueTimeStartProperty =
          DependencyProperty.Register("ValueTimeStart", typeof(DateTime), typeof(ucDatePicker), new PropertyMetaData(DateTime.Now));

    public DateTime ValueTimeEnd
    {
      get { return (DateTime)GetValue(ValueTimeEndProperty); }
      set { SetValue(ValueTimeEndProperty, true); }
    }
    public static readonly DependencyProperty ValueTimeEndProperty =
          DependencyProperty.Register("ValueTimeEnd", typeof(DateTime), typeof(ucDatePicker), new PropertyMetaData(DateTime.Now));
          
    #endregion
    
  
    public bool IsValid
    {
      get { return GetFrom <= GetTo; }
    }
    
    public DateTime GetFrom 
    {
      get { return MergeDateTime(SelectedDateStart, ValueTimeStart); }
    }
    
    public DateTime GetTo
    {
      get { return MergeDateTime(SelectedDateEnd, ValueTimeEnd); }
    }
    
    private bool m_valueChanged = false;
    
    /// <summary>
    /// Occurs when the value in any of the sub controls is changed
    /// </summary>
    public bool ValueChanged { get return m_valueChanged; } }
    
    public ucDatePicker()
    {
      InitializeComponent();
    }
    
    /// <summary>
    /// Merge Date and Time variables
    /// </summary>
    private DateTime MergeDateTime(DateTime dt_date, DateTime dt_time)
    {
      if (dt_date == null || dt_time == null) return DateTime.Now;
      
      return new DateTime(
        dt_date.Year,
        dt_date.Month,
        dt_date.Day,
        dt_time.Hour,
        dt_time.Minute, 0);
    }
  
  
  #region Event Handling
  
  private void fromDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e) { m_valueChanged = true; }
  private void fromTime_ValueChanged(object sender, Telerik.Windows.RadRoutedEventArgs e) { m_valueChanged = true; }
  
  private void toDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e) { m_valueChanged = true; }
  private void toTime_ValueChanged(object sender, Telerik.Windows.RadRoutedEventArgs e) { m_valueChanged = true; }
  
  /// <summary>
  /// UserControl's LostFocus event handling
  /// </summary>
  private void RootDateTimeFromToFields_LostFocus(object sender, RoutedEventArgs e)
  {
    //If focus is still within the usercontrol - so don't fire the LostFocus event
    if (this.IsKeyboardFocusWithin || !this.ValueChanged)
      e.Handled = true;
    
    //Reset flag when lostfocus is from the usercontrol
    if (e.Handled == false && this.ValueChanged)
      m_valueChanged = false;
  }
  
  #endregion
  
}
  
  public class ValidateDateTimeConverter : IMultiValueConverter
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
         var from = //startDate.Add(startTime.TimeOfDay);
                    new DateTime(startDate.Year, startDate.Month, startDate.Day,
                                 startTime.Hour, startTime.Minute, 0);
         var to = //endDate.Add(endTime.TimeOfDay);
                  new DateTime(endDate.Year, endDate.Month, endDate.Day,
                                endTime.Hour, endTime.Minute, 0);
         
         return Condition(from, to) ? Brushes.Transparent : Brushes.Red;
      }
      return Brushes.Transparent;
    }
    
    private static bool Condition(DateTime startDate, DateTime endDate) 
    { 
       return startDate<= endDate; 
    }
    
    pulbic object[] Convertback(object value, Type[] targetType, object parmaeter, CultureIfo culture)
    {
       throw new NotImplementedException();
    }
  }
}
