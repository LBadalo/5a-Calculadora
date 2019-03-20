using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Calculadora.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            //Iniciar o visor com o '0'
            ViewBag.Ecra = "0";
            //Vars auxiliares
            Session["primeiraVezOperador"] = true;
            return View();
        }

        //POST: Home
        [HttpPost]
        public ActionResult Index(string bt, string visor)
        {
            //var auxiliar
            string ecra = visor;
            string trocavalor = ecra.Substring(1);

            //Identificar o valor na variável 'bt'
            switch (bt)
            {
            
                //Se entrei aqui, é porque foi selecionado um 'algarismo'
                case "0":
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":

                    //Vou decidir o que fazer quando o visor só existe o 'zero'
                    
                    if (visor.Equals("0")) //visor =="0"
                    {
                        ecra = bt;
                    }
                    else
                    {
                        ecra = ecra + bt;
                    }
                            
                    break;
                case ",":
                    //Processar o caso da ','
                    if (!visor.Contains(",")) ecra = ecra + bt;
                    break;
                //Se entrei aqui, é porque foi selecionado um 'operador'
                case "+":
                case "-":
                case "x":
                case ":":
                
                    //É a primeira vez que carreguei num destes operadores?
                    if ((bool)Session["primeiraVezOperador"] == true) { // == true é facultativo

                    };
                    break;
                //Apagar o valor dentro do ecrã
                case "C":
                    ecra = "";
                break;
                //Alterar o valor de positivo para negativo
                case "+/-":
                    if (!visor.Contains("-"))
                        ecra = "-" + visor;
                    else
                        ecra = trocavalor;
                    break;

            }
            //prepara o conteúdo a aparecer no VISOR da View
            ViewBag.Ecra = ecra;





            return View();
        }
    }
}