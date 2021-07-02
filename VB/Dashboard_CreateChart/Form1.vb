Imports System
Imports System.Windows.Forms
Imports DevExpress.DashboardCommon
Imports DevExpress.DataAccess

Namespace Dashboard_CreateChart
	Partial Public Class Form1
		Inherits Form

		Public Sub New()
			InitializeComponent()
		End Sub
		Private Function CreateChart(ByVal dataSource As DashboardObjectDataSource) As ChartDashboardItem

			' Creates a chart dashboard item and specifies its data source.
			Dim chart As New ChartDashboardItem()
			chart.DataSource = dataSource

			' Specifies the dimension used to provide data for arguments on a chart.
			chart.Arguments.Add(New Dimension("Sales Person", DateTimeGroupInterval.Year))

			' Specifies the dimension that provides data for chart series.
			chart.SeriesDimensions.Add(New Dimension("OrderDate"))

			' Adds a new chart pane to the chart's Panes collection.
			chart.Panes.Add(New ChartPane())
			' Creates a new series of the Bar type.
			Dim salesAmountSeries As New SimpleSeries(SimpleSeriesType.Bar)
			' Specifies the measure that provides data used to calculate
			' the Y-coordinate of data points.
			salesAmountSeries.Value = New Measure("Extended Price")
			' Adds created series to the pane's Series collection to display within this pane.
			chart.Panes(0).Series.Add(salesAmountSeries)

			chart.Panes.Add(New ChartPane())
			Dim taxesAmountSeries As New SimpleSeries(SimpleSeriesType.StackedBar)
			taxesAmountSeries.Value = New Measure("Quantity")
			chart.Panes(1).Series.Add(taxesAmountSeries)

			Return chart
		End Function
		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

			' Creates a dashboard and sets it as the currently opened dashboard in the dashboard viewer.
			dashboardViewer1.Dashboard = New Dashboard()

			' Creates a data source and adds it to the dashboard data source collection.
			Dim dataSource As New DashboardObjectDataSource()
			dataSource.DataSource = (New nwindDataSetTableAdapters.SalesPersonTableAdapter()).GetData()
			dashboardViewer1.Dashboard.DataSources.Add(dataSource)

			' Creates a chart dashboard item with the specified data source 
			' and adds it to the Items collection to display within the dashboard.
			Dim chart As ChartDashboardItem = CreateChart(dataSource)
			dashboardViewer1.Dashboard.Items.Add(chart)

			' Reloads data in the data sources.
			dashboardViewer1.ReloadData()
		End Sub
	End Class
End Namespace
