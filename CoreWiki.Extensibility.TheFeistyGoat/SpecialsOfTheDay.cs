using CoreWiki.Extensibility.Common;
using System.Text;

namespace CoreWiki.Extensibility.TheFeistyGoat
{
    public class SpecialsOfTheDay : ICoreWikiModule
    {
        void ICoreWikiModule.Initialize(CoreWikiModuleEvents moduleEvents)
        {
            moduleEvents.PreSubmitArticle += OnPreSubmitArticle;
        }

        void OnPreSubmitArticle(PreSubmitArticleEventArgs e)
        {
            // get specials from a data store
            var specials = SpecialItem.GetSpecials();

            StringBuilder builder = new StringBuilder();
            builder.AppendLine();
            builder.AppendLine("------- The Feisty Goat :: daily specials -------");
            foreach (var item in specials)
                builder.AppendLine(string.Format("{0} - regular price {1:#.00}, today: {2:#.00}", item.Item, item.RegularPrice, item.SpecialPrice));

            e.Content += builder.ToString();
        }
    }
}
