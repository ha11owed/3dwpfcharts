using WpfPlot.Model.Impl;
using WpfPlot.Plot3d;

namespace WpfPlot
{
    /// <summary>
    /// Provides a ViewModel for the Main window.
    /// </summary>
    public class MainViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        public MainViewModel()
        {
            Plot = new APlotVM(new Plot());
        }

        /// <summary>
        /// Gets or sets the model.
        /// </summary>
        /// <value>The model.</value>
        public APlotVM Plot { get; set; }

        //public Model3D Create3dPoint(Point3D point, double radius,)
    }
}