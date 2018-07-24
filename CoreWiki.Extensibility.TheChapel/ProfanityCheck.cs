using CoreWiki.Extensibility.Common;
using System;

namespace CoreWiki.Extensibility.TheChapel
{
    public class ProfanityCheck : ICoreWikiModule
    {
        public ProfanityCheck()
        {
            _BadWords = GetProfanityWords();
        }

        void ICoreWikiModule.Initialize(CoreWikiModuleEvents moduleEvents)
        {
            moduleEvents.PreSubmitArticle += OnPreSubmitArticle;
        }

        string[] _BadWords;

        void OnPreSubmitArticle(PreSubmitArticleEventArgs e)
        {
            e.Topic = RemoveProfanity(e.Topic);
            e.Content = RemoveProfanity(e.Content);
        }

        string RemoveProfanity(string text)
        {
            string newText = text;

            foreach (string badWord in _BadWords)
                newText = newText.Replace(badWord, "$%!@&*#$");

            return newText;
        }

        string[] GetProfanityWords()
        {
            return new string[]
            {
                 "filth", "flarn"
            };
        }
    }
}
