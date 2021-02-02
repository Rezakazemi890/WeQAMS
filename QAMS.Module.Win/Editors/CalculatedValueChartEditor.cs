using DevExpress.ExpressApp.Chart.Win;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.Utils;
using DevExpress.XtraCharts;
using QAMS.Module.Editors.ChartEditor;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QAMS.Module.Win.Editors
{
    [PropertyEditor(typeof(CalculatedValueForChart), "CalculatedValueChartEditor", true)]
    public class CalculatedValueChartEditor : ChartPropertyEditor
    {
        public CalculatedValueChartEditor(Type objectType, IModelMemberViewItem info)
     : base(objectType, info)
        {
            info.Caption = "";
        }

        protected override object CreateControlCore()
        {
            return base.CreateControlCore();
        }

        protected override void OnControlCreated()
        {
            base.OnControlCreated();

            this.ChartControl.CrosshairEnabled = DevExpress.Utils.DefaultBoolean.True;
            this.ChartControl.ToolTipEnabled = DevExpress.Utils.DefaultBoolean.True;
            this.ChartControl.SeriesTemplate.View = new SideBySideBarSeriesView();
            this.ChartControl.SeriesTemplate.ArgumentDataMember = "Argument";
            this.ChartControl.SeriesTemplate.SeriesDataMember = "Series";
            this.ChartControl.SeriesTemplate.ValueDataMembers.AddRange(new string[] { "Value" });
            this.ChartControl.SeriesTemplate.ValueScaleType = ScaleType.Numerical;
            this.ChartControl.SeriesTemplate.ShowInLegend = true;
            this.ChartControl.SeriesTemplate.ToolTipHintDataMember = "ToolTip";
            this.ChartControl.SeriesTemplate.ToolTipPointPattern = "{HINT} {A}";
            this.ChartControl.SeriesTemplate.LabelsVisibility = DefaultBoolean.True;
            this.ChartControl.CustomDrawSeriesPoint += ChartControl_CustomDrawSeriesPoint;
            this.ChartControl.CustomDrawCrosshair += ChartControl_CustomDrawCrosshair;
            BuildChart();
        }

        void ChartControl_CustomDrawCrosshair(object sender, CustomDrawCrosshairEventArgs e)
        {
            foreach (CrosshairElement element in e.CrosshairElements)
            {
                SeriesPoint currentPoint = element.SeriesPoint;

                if (currentPoint.Tag.GetType() == typeof(DataRowView))
                {
                    DataRowView rowView = (DataRowView)currentPoint.Tag;
                    string s = rowView["ToolTip"].ToString();

                    element.LabelElement.Text = s;

                }
            }
        }

        //برای نمایش تعداد بر روی آیتم های چارت
        void ChartControl_CustomDrawSeriesPoint(object sender, CustomDrawSeriesPointEventArgs e)
        {
            e.Series.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            e.Series.ValueScaleType = ScaleType.Numerical;
            e.LabelText = ((DataRowView)e.SeriesPoint.Tag)["LableText"].ToString();
        }

        protected override void OnValueRead()
        {
            base.OnValueRead();

            BuildChart();
        }

        private void BuildChart()
        {

            #region Define DataSource
            while (this.ChartControl.Series.Count > 0)
                this.ChartControl.Series.Remove(this.ChartControl.Series[0]);

            if (this.PropertyValue == null)
                return;

            DataTable table = new DataTable("Table1");
            table.Columns.Add("Argument", typeof(string));
            table.Columns.Add("Series", typeof(string));
            table.Columns.Add("Value", typeof(Int32));
            table.Columns.Add("GroupName", typeof(Int32));
            table.Columns.Add("ToolTip", typeof(string));
            table.Columns.Add("LableText", typeof(string));

            if (this.PropertyValue != null)
            {
                CalculatedValueForChart calculatedValueForChart = this.PropertyValue as CalculatedValueForChart;
                AddDataToDataTable(table, calculatedValueForChart);
            }

            this.ChartControl.DataSource = table;


            #endregion
        }
        private static void AddDataToDataTable(DataTable table, CalculatedValueForChart CalculatedValueForChart)
        {
            if (CalculatedValueForChart != null)
            {
                List<CalculatedValueForChartItem> items = CalculatedValueForChart.CalculatedValueForChartItems;
                // Add data rows to the table.
                DataRow row = null;
                foreach (CalculatedValueForChartItem item in items)
                {
                    row = table.NewRow();
                    row["Argument"] = item.ChartArgument;
                    row["Series"] = item.ChartSeries;
                    row["Value"] = item.ChartValue;
                    row["GroupName"] = item.ChartArgument;
                    row["ToolTip"] = item.ChartArgument + " " + item.ChartValue.ToString();
                    row["LableText"] = item.ChartValue.ToString();
                    table.Rows.Add(row);
                }
            }
        }

        protected override object GetControlValueCore()
        {
            return null;
        }
    }
}
