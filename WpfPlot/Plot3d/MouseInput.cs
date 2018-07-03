using System.Windows.Input;
using System.Windows.Media.Media3D;

namespace WpfPlot.Plot3d
{
    public class MouseInput
    {
        public MouseInput()
        {
            Hits = new Model3D[0];
        }

        public MouseInput(MouseButtonState leftButton, MouseButtonState middleButton, MouseButtonState rightButton, Point3D? position, Model3D[] hits)
        {
            LeftButton = leftButton;
            MiddleButton = middleButton;
            RightButton = rightButton;
            Position = position;
            Hits = hits;
        }

        public Model3D[] Hits { get; private set; }

        /// <summary>
        /// Gets the current state of the left mouse button.
        /// </summary>
        public MouseButtonState LeftButton { get; private set; }

        /// <summary>
        /// Gets the current state of the middle mouse button.
        /// </summary>
        public MouseButtonState MiddleButton { get; private set; }

        /// <summary>
        /// The position of the mouse in 3d space.
        /// </summary>
        public Point3D? Position { get; private set; }

        /// <summary>
        /// Gets the current state of the right mouse button.
        /// </summary>
        public MouseButtonState RightButton { get; private set; }
    }
}