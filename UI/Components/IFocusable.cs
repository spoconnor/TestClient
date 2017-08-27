using TestClient.Utilities;
using TestClient.Library;

namespace TestClient.UI.Components
{
    interface IFocusable
    {
        void Focus();
        void Unfocus();

        event GenericEventHandler<IFocusable> Focused; 
        event GenericEventHandler<IFocusable> Unfocused;
    }
}
