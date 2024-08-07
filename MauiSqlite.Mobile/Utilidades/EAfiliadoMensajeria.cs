using CommunityToolkit.Mvvm.Messaging.Messages;

namespace MauiSqlite.Mobile.Utilidades
{
    public class EAfiliadoMensajeria : ValueChangedMessage<EAfiliadoMensaje>
    {
        public EAfiliadoMensajeria(EAfiliadoMensaje value) : base(value)
        {

        }
    }
}
