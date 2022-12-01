namespace Mission_PrincessRescue
{
    abstract class Controller
    {
        protected int index;

        public Controller(int controllerIndex)
        {
            index = controllerIndex;
        }

        public abstract bool IsFirePressed();
        public abstract bool IsJumpPressed();
        public abstract float GetHorizontal();
        public abstract float GetVertical();

    }
}