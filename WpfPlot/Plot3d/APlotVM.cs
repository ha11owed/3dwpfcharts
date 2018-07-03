using System;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Media.Media3D;
using WpfPlot.Model;

namespace WpfPlot.Plot3d
{
    public class APlotVM : INotifyPropertyChanged
    {
        private readonly IPlot _plot;
        private readonly PlotPoints _plotPoints;
        private KeyboardInput _lastKeyboardInput;
        private MouseInput _lastMouseInput;
        private Model3DGroup _model3dGroup;
        private SelectionManager _selection;
        private Vector3D _step;

        public APlotVM(IPlot plot)
        {
            _plot = plot;
            _lastKeyboardInput = new KeyboardInput();
            _lastMouseInput = new MouseInput();
            _selection = new SelectionManager(plot);
            _plotPoints = new PlotPoints();

            Update();
            Step = new Vector3D(0, 0, 1);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public KeyboardInput LastKeyboardInput
        {
            get { return _lastKeyboardInput; }
        }

        public MouseInput LastMouseInput
        {
            get { return _lastMouseInput; }
        }

        public Model3D Model3d
        {
            get { return _model3dGroup; }
        }

        public Vector3D Step
        {
            get { return _step; }
            set { _step = value; }
        }

        public virtual bool CreatePoints(MouseInput mouseInput)
        {
            bool result = false;
            if (_lastKeyboardInput.Key == Key.None)
            {
                //PointAdd add = new PointAdd(Helpers.Point3DToPosition(mouseInput.Position), null, null);
                //result = (_plot.AddPoints(new PointAdd[] { add }) != EditResult.Invalid);
            }
            return result;
        }

        public void OnKeyEvent(KeyEventArgs e)
        {
            KeyState keyState = KeyState.None;
            if (e.IsUp)
                keyState = KeyState.Up;
            else if (e.IsDown)
                keyState = KeyState.Down;

            KeyboardInput keyboardInput = new KeyboardInput(e.Key, keyState, Keyboard.Modifiers);
            UserInput(keyboardInput);
        }

        public void Update()
        {
            Model3DGroup group = new Model3DGroup();
            _plotPoints.AddPoints(_plot, group);
            _model3dGroup = group;

            OnPropertyChanged(nameof(Model3d));
        }

        public void UserInput(KeyboardInput keyboardInput)
        {
            bool updateRequired = true;
            for (;;)
            {
                if (_selection.SelectedPoints.Length == 0)
                    break;

                if (DeletePoints(keyboardInput, _selection.SelectedPoints))
                    break;

                if (MovePoints(keyboardInput, _selection.SelectedPoints))
                    break;

                updateRequired = false;
                break;
            }

            _lastKeyboardInput = keyboardInput;

            // Update the GUI if needed.
            if (updateRequired)
                Update();
        }

        public void UserInput(MouseInput mouseInput)
        {
            if (mouseInput.Position != null)
            {
                if (IsSelectionStart(mouseInput))
                {
                    if (_selection.SelectedPoints.Length == 0)
                    {
                        // There is no selection, we can start a new one
                        _selection.Start(mouseInput.Position.Value);
                    }
                    else
                    {
                    }
                }
                else if (IsSelectionUpdate(mouseInput))
                {
                    _selection.Update(mouseInput.Position.Value);
                }
                else if (IsSelectionEnd(mouseInput))
                {
                    //_selection.Clear();
                }
            }

            _lastMouseInput = mouseInput;
            Update();

            if (mouseInput.Hits != null)
            {
                // We have selection, decide what to do with it.
                int n = mouseInput.Hits.Length;
                int nPoints = 0;
                IPoint[] selectedPoints = new IPoint[n];
                for (int i = 0; i < n; i++)
                {
                    IPoint point = _plotPoints.GetPoint(mouseInput.Hits[i]);
                    if (point == null)
                        break;

                    nPoints++;
                    selectedPoints[i] = point;
                }

                if (nPoints != n)
                    return;

                //if (mouseInput.MoveDelta.Length > 0)
                {
                    //MovePoints(mouseInput.MoveDelta, selectedPoints);
                    return;
                }
            }
            else
            {
                // No selection, the user is free to create points
                CreatePoints(mouseInput);
            }
        }

        protected virtual bool DeletePoints(KeyboardInput userInput, IPoint[] points)
        {
            CheckPoints(points);

            bool result = false;
            if (LastKeyboardInput.Key == Key.Delete && LastKeyboardInput.KeyState == KeyState.Down
                && userInput.Key == Key.Delete && userInput.KeyState == KeyState.Up
                && userInput.ModifierKeys == ModifierKeys.None)
            {
                result = (_plot.DeletePoints(points) != EditResult.Invalid);
            }
            return result;
        }

        protected virtual bool IsSelectionEnd(MouseInput mouseInput)
        {
            return (mouseInput.LeftButton == MouseButtonState.Released && LastMouseInput.LeftButton == MouseButtonState.Pressed);
        }

        protected virtual bool IsSelectionStart(MouseInput mouseInput)
        {
            return (mouseInput.LeftButton == MouseButtonState.Pressed && LastMouseInput.LeftButton == MouseButtonState.Released);
        }

        protected virtual bool IsSelectionUpdate(MouseInput mouseInput)
        {
            return (mouseInput.LeftButton == MouseButtonState.Pressed && LastMouseInput.LeftButton == MouseButtonState.Pressed);
        }

        protected virtual bool MovePoints(KeyboardInput userInput, IPoint[] points)
        {
            CheckPoints(points);

            bool result = false;
            if (userInput.KeyState == KeyState.Down)
            {
                Vector3D moveDelta = new Vector3D(0, 0, 0);
                switch (userInput.Key)
                {
                    case Key.Up:
                    case Key.U:
                        moveDelta = new Vector3D(0, 0, 1);
                        break;

                    case Key.Down:
                    case Key.J:
                        moveDelta = new Vector3D(0, 0, -1);
                        break;
                }

                // Call the move method with a vector.
                if (moveDelta.LengthSquared > 0)
                {
                    result = MovePoints(moveDelta, points);
                }
            }
            return result;
        }

        protected virtual bool MovePoints(Vector3D moveDelta, IPoint[] points)
        {
            CheckPoints(points);

            PointMove[] moves = new PointMove[points.Length];
            for (int i = 0; i < points.Length; i++)
            {
                IPoint point = points[i];
                Position newPosition = Helpers.Move(point.Position, moveDelta);
                moves[i] = new PointMove(point, newPosition);
            }
            bool result = (_plot.MovePoints(moves) != EditResult.Invalid);
            return result;
        }

        private static void CheckPoints(IPoint[] points)
        {
            if (points == null || points.Length == 0) { throw new ArgumentException(nameof(points)); }
        }

        private void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(name));
        }
    }
}