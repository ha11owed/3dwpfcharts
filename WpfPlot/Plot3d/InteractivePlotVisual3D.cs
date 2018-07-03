using HelixToolkit.Wpf;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Media3D;
using static HelixToolkit.Wpf.Viewport3DHelper;

namespace WpfPlot.Plot3d
{
    public class InteractivePlotVisual3D : UIElement3D
    {
        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
            "ViewModel",
            typeof(APlotVM), typeof(InteractivePlotVisual3D),
            new UIPropertyMetadata(null, OnViewModelChanged));

        public InteractivePlotVisual3D()
        {
        }

        public APlotVM ViewModel
        {
            get { return (APlotVM)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            OnMouseAnyEvent(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            OnMouseAnyEvent(e);
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);
            OnMouseAnyEvent(e);
        }

        private static void OnViewModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            InteractivePlotVisual3D sender = (InteractivePlotVisual3D)d;
            sender.OnSetViewModel((APlotVM)e.OldValue);
        }

        private void OnMouseAnyEvent(MouseEventArgs e)
        {
            APlotVM viewModel = ViewModel;
            if (viewModel == null)
                return;

            Viewport3D viewPort = this.GetViewport();
            Point mousePosition = e.GetPosition(this);
            Ray3D ray = viewPort.GetRay(mousePosition);
            IList<HitResult> hitResult = viewPort.FindHits(mousePosition);
            Model3D[] hits = new Model3D[hitResult.Count];
            for (int i = 0; i < hitResult.Count; i++)
            {
                hits[i] = hitResult[i].Model;
            }

            MouseInput mouseInput = new MouseInput(e.LeftButton, e.MiddleButton, e.RightButton,
                ray.PlaneIntersection(new Point3D(), new Vector3D(0, 0, 1)),
                hits);
            viewModel.UserInput(mouseInput);
        }

        private void OnSetViewModel(APlotVM viewModelOld)
        {
            if (viewModelOld != null)
            {
                viewModelOld.PropertyChanged -= PlotPropertyChanged;
            }

            if (ViewModel != null)
            {
                ViewModel.PropertyChanged += PlotPropertyChanged;
                Visual3DModel = ViewModel.Model3d;
            }
            else
            {
                Visual3DModel = null;
            }
        }

        private void PlotPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(APlotVM.Model3d))
            {
                Visual3DModel = ViewModel.Model3d;
            }
        }
    }
}