using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using DynamicMenu.Web.Models;

namespace DynamicMenu.Web.Controllers
{
    public class HomeController : Controller
    {
        //Lista que armazena o menu pesquisado no banco
        List<Menu> menuLista = null;

        [HttpGet]
        public ActionResult Index()
        {
            string meuMenu = string.Empty;

            //Pesquisa os dados para montagem do menu
            menuLista = PesquisarDadosMenu();

            //Caso encontre informações...
            if (menuLista != null && menuLista.Count > 0)
            {
                meuMenu = CriarMenu();
            }

            return View((object)meuMenu);
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        #region CriarMenu

        /// <summary>
        /// Inicia a criação do menu percorrendo os itens roots
        /// </summary>
        /// <returns>string com menu construído</returns>
        private string CriarMenu()
        {
            StringWriter stringWriter = new StringWriter();
            HtmlTextWriter htmlWriter = new HtmlTextWriter(stringWriter);

            //Inicia o menu
            htmlWriter.RenderBeginTag(HtmlTextWriterTag.Ul);

            //Percorre apenas o nível root, ou seja, o nível que não tem pai
            foreach (var menuItem in menuLista.Where(m => m.MenuFather == 0))
            {
                CriarSubMenu(htmlWriter, menuItem);
            }

            //finaliza o menu
            htmlWriter.RenderEndTag(); //UL

            return stringWriter.ToString();
        }

        #endregion //CriarMenu

        #region CriarSubMenu

        /// <summary>
        /// Criar a estrutura do menu com seus respectivos itens e sub itens
        /// </summary>
        /// <param name="htmlWriter">Escritor das tags html</param>
        /// <param name="itemCorrente">Item corrente do menu</param>
        private void CriarSubMenu(HtmlTextWriter htmlWriter, Menu itemCorrente)
        {
            try
            {
                //Pequisa os filhos do item corrente do menu
                var listaFilhos = menuLista.Where(m => m.MenuFather == itemCorrente.Id);

                // booleano que indica se o item corrente tem filho
                bool temFilho = listaFilhos.Count() > 0;

                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Li);

                MontarItemMenu(htmlWriter, itemCorrente);

                if (temFilho)
                    htmlWriter.RenderBeginTag(HtmlTextWriterTag.Ul);

                foreach (var itemFilho in listaFilhos)
                {
                    //para cada filho do item corrente, aciona novamente o próprio método passando o itemFilho como itemCorrente
                    CriarSubMenu(htmlWriter, itemFilho);
                }

                if (temFilho)
                    htmlWriter.RenderEndTag(); //UL

                htmlWriter.RenderEndTag(); //LI
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion //CriarSubMenu

        #region MontarItemMenu
        /// <summary>
        /// Cria formatações, link e demais detalhes dos itens do menu
        /// </summary>
        /// <param name="htmlWriter">Escritor das tags html</param>
        /// <param name="itemCorrente"></param>
        private static void MontarItemMenu(HtmlTextWriter htmlWriter, Menu itemCorrente)
        {
            if (!string.IsNullOrEmpty(itemCorrente.Link))
            {
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Href, itemCorrente.Link);
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.A);
                htmlWriter.Write(itemCorrente.Name);
                htmlWriter.RenderEndTag(); //A
            }
            else
            {
                htmlWriter.Write(itemCorrente.Name);
            }
        }

        #endregion //MontarItemMenu

        #region PesquisarDadosMenu
        /// <summary>
        /// Simula uma pesquina do menu no bando de dados.
        /// </summary>
        /// <returns></returns>
        private List<Menu> PesquisarDadosMenu()
        {
            List<Menu> menuLista = new List<Menu>();

            menuLista.Add(new Menu(1, "Dicas do Sr. Coelho", string.Empty, 0));
            menuLista.Add(new Menu(2, "Dica1", "http://srcoelho.com.br/2013/01/11/dica-para-obter-desconto-na-compra-online", 1));
            menuLista.Add(new Menu(3, "Dica2", "http://srcoelho.com.br/2012/12/23/imagens-no-gmail", 1));
            menuLista.Add(new Menu(4, "Dica3", "http://srcoelho.com.br/2012/08/26/dicas-transito", 1));
            menuLista.Add(new Menu(5, "Dica4", "http://srcoelho.com.br/2011/09/10/teste-de-velocidade-de-internet", 1));

            menuLista.Add(new Menu(6, "Artigos do Sr. Coelho", string.Empty, 0));
            menuLista.Add(new Menu(7, "Artigo 1", string.Empty, 6));
            menuLista.Add(new Menu(8, "Artigo 1.1", string.Empty, 7));
            menuLista.Add(new Menu(9, "Artigo 1.1.1", string.Empty, 8));
            menuLista.Add(new Menu(10, "Artigo 1.1.2", string.Empty, 8));

            menuLista.Add(new Menu(11, "Artigo 2", string.Empty, 6));
            menuLista.Add(new Menu(12, "Artigo 2.1", string.Empty, 11));
            menuLista.Add(new Menu(13, "Artigo 2.2", string.Empty, 11));
            menuLista.Add(new Menu(14, "Artigo 2.3", string.Empty, 11));
            menuLista.Add(new Menu(15, "Artigo 2.3.1", string.Empty, 14));

            return menuLista;
        }

        #endregion //PesquisarDadosMenu
    }
}