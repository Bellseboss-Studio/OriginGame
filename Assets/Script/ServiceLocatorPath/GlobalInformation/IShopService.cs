using System.Collections.Generic;

namespace SystemOfExtras.GlobalInformationPath
{
    public interface IShopService
    {
        List<ElementInShop> GetElements();
        void Buy(IElement element);
        void Buy(ElementInShop element);
    }
}