using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Web;
using DevExpress.ExpressApp.Web.Editors;
using DevExpress.Utils;
using DevExpress.XtraCharts;
using DevExpress.XtraCharts.Web;
using QAMS.Module.Editors.ChartEditor;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace QAMS.Module.Web.Editors
{
    [PropertyEditor(typeof(CalculatedValueForChart), "CalculatedValueChartEditor", true)]
    public class CalculatedValueChartEditor : WebPropertyEditor
    {
        public CalculatedValueChartEditor(Type objectType, IModelMemberViewItem model) : base(objectType, model)
        {
            model.Caption = "";
        }

        protected override WebControl CreateEditModeControlCore()
        {
            return BuildChart();
        }

        protected override WebControl CreateViewModeControlCore()
        {
            return BuildChart();
        }

        WebChartControl _baseChart;
        //Series mainSeries;
        public WebChartControl BuildChart()
        {
            //  ASPxRoundPanel panel =  RenderHelper.CreateASPxRoundPanel();
            //   panel.Width = Unit.Percentage(100);
            //   panel.Height = Unit.Percentage(100);

            _baseChart = new WebChartControl();
            //   panel.Controls.Add(baseChart);
            //   baseChart.Width = new Unit(Convert.ToInt32(panel.Width.Value));
            // baseChart.Height = new Unit(Convert.ToInt32(panel.Height.Value));

            RenderHelper.SetupASPxWebControl(_baseChart);
            _baseChart.CssClass = "xafChart";
            _baseChart.Width = Unit.Pixel(400);
            _baseChart.Height = Unit.Pixel(220);

            _baseChart.EnableCallBacks = false;
            _baseChart.SeriesDataMember = "GroupName";
            _baseChart.CrosshairEnabled = DevExpress.Utils.DefaultBoolean.False;
            _baseChart.ToolTipEnabled = DevExpress.Utils.DefaultBoolean.True;
            _baseChart.SeriesTemplate.View = new SideBySideBarSeriesView();
            _baseChart.SeriesTemplate.SeriesDataMember = "Series";
            _baseChart.SeriesTemplate.ArgumentDataMember = "Argument";
            _baseChart.SeriesTemplate.ValueDataMembers.AddRange("Value");
            _baseChart.SeriesTemplate.ValueScaleType = ScaleType.Numerical;
            _baseChart.CrosshairOptions.CrosshairLabelMode = CrosshairLabelMode.ShowForEachSeries;
            _baseChart.SeriesTemplate.ShowInLegend = false;
            _baseChart.SeriesTemplate.ColorDataMember = "Argument";
            _baseChart.SeriesTemplate.ToolTipHintDataMember = "ToolTip";
            _baseChart.SeriesTemplate.ToolTipPointPattern = "{HINT}";
            _baseChart.SeriesTemplate.LabelsVisibility = DefaultBoolean.True;
            _baseChart.DataSource = CreateChartData();
            _baseChart.CustomDrawSeriesPoint += baseChart_CustomDrawSeriesPoint;
            _baseChart.CustomDrawAxisLabel += baseChart_CustomDrawAxisLabel;
            _baseChart.CustomDrawSeries += baseChart_CustomDrawSeries;
            return _baseChart;
        }

        void baseChart_CustomDrawSeries(object sender, CustomDrawSeriesEventArgs e)
        {
            //  e.Series.CrosshairLabelPattern = ((DataRowView)e.Series.Tag)["ToolTip"].ToString();
            return;
        }

        private void baseChart_CustomDrawAxisLabel(object sender, CustomDrawAxisLabelEventArgs e)
        {
            //برای اینکه به صورت اعشاری نشان ندهد لیبل اعداد اعشاری را نال کردم
            AxisBase axis = e.Item.Axis;
            if (axis is AxisY || axis is AxisY3D || axis is RadarAxisY)
            {
                double axisValue = (double)e.Item.AxisValue;
                if (axisValue != 0 && axisValue != 1 && axisValue != 2 && axisValue != 3 && axisValue != 4 && axisValue != 5 && axisValue != 6 && axisValue != 7
                    && axisValue != 8 && axisValue != 9 && axisValue != 10 && axisValue != 11 && axisValue != 12 && axisValue != 13 && axisValue != 14 && axisValue != 15 && axisValue != 16
                    && axisValue != 17 && axisValue != 18 && axisValue != 19 && axisValue != 20 && axisValue != 21 && axisValue != 22 && axisValue != 23 && axisValue != 24 && axisValue != 25
                    && axisValue != 26 && axisValue != 27 && axisValue != 28 && axisValue != 29 && axisValue != 30 && axisValue != 31 && axisValue != 32 && axisValue != 33 && axisValue != 34
                    && axisValue != 35 && axisValue != 36 && axisValue != 37 && axisValue != 38 && axisValue != 39 && axisValue != 40)
                {
                    e.Item.Text = "";
                }
            }
        }

        private void baseChart_CustomDrawSeriesPoint(object sender, CustomDrawSeriesPointEventArgs e)
        {
            e.Series.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            e.Series.ValueScaleType = ScaleType.Numerical;
            e.LabelText = ((DataRowView)e.SeriesPoint.Tag)["LableText"].ToString();
            e.Series.CrosshairLabelPattern = ((DataRowView)e.SeriesPoint.Tag)["ToolTip"].ToString();

            return;
        }

        private DataTable CreateChartData()
        {
            // Create an empty table.
            DataTable table = new DataTable("Table1");
            // Add two columns to the table.
            table.Columns.Add("Argument", typeof(string));
            table.Columns.Add("Series", typeof(string));
            table.Columns.Add("GroupName", typeof(string));
            table.Columns.Add("Value", typeof(double));
            table.Columns.Add("ToolTip", typeof(string));
            table.Columns.Add("LableText", typeof(string));
            if (this.PropertyValue != null)
            {
                CalculatedValueForChart calculatedValueForChart = this.PropertyValue as CalculatedValueForChart;
                AddDataToDataTable(table, calculatedValueForChart);
            }
            return table;
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

        protected override void ReadEditModeValueCore()
        {
            UpdateChartData();
        }

        protected override void ReadViewModeValueCore()
        {
            UpdateChartData();
        }

        private void UpdateChartData()
        {
            if (_baseChart != null)
            {
                _baseChart.DataSource = CreateChartData();
                _baseChart.DataBind();
            }
        }

        protected override object GetControlValueCore()
        {
            return null;
        }
    }
}
