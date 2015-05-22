using JadeX.HtmlMinifier.Models;
using Orchard.ContentManagement.Handlers;

namespace JadeX.HtmlMinifier.Handlers
{
    public class MinificationSettingsPartHandler : ContentHandler
    {
        public MinificationSettingsPartHandler()
        {
            Filters.Add(new ActivatingFilter<MinificationSettingsPart>("Site"));
        }
    }
}
