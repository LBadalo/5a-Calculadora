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
            Session["iniciarOperando"] = false;

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

                    if ((bool)Session["iniciarOperando"] || visor.Equals("0")) //visor =="0"
                    {
                        ecra = bt;
                    }
                    else
                    {
                        ecra = ecra + bt;
                    }
                    Session["iniciarOperando"] = false;
                    break;
                //Alterar o valor de positivo para negativo
                case "+/-":
                    if (!visor.Contains("-"))
                    {
                        ecra = "-" + visor;
                    }
                    else
                    {
                        ecra = trocavalor;
                    }
                    break;
                case ",":
                    //Processar o caso da ','
                    if (!visor.Contains(","))
                    {
                        ecra = ecra + bt;
                    }
                    break;
                //Se entrei aqui, é porque foi selecionado um 'operador'
                case "+":
                case "-":
                case "x":
                case ":":
                case "=":
                    //É a primeira vez que carreguei num destes operadores?
                    if ((bool)Session["primeiraVezOperador"]) // == true é facultativo
                    { 
                        Session["primeiraVezOperador"] = false;
                    }
                    else
                    {
                        // Recuperar os valores dos operandos
                        double operador1 = Convert.ToDouble((string)Session["primeiroOperando"]);
                        double operador2 = Convert.ToDouble(visor);
                        switch ((string)Session["operadorAnterior"])
                        {
                            case "+":
                                visor = operador1 + operador2 + "";
                                break;
                            case "-":
                                visor = operador1 - operador2 + "";
                                break;
                            case "x":
                                visor = operador1 * operador2 + "";
                                break;
                            case ":":
                                visor = operador1 / operador2 + "";
                                break;
                        }

                    }
                    ecra = visor;
                    Session["primeiroOperando"] = visor;
                    // limpar display
                    Session["iniciarOperando"] = true;

                    if (bt.Equals("="))
                    {
                        //Marcar o operador como primeiro operando
                        Session["primeiraVezOperador"] = true;
                    }
                    else
                    {
                        // Guardar o valor do operador
                        Session["operadorAnterior"] = bt;
                        Session["primeiraVezOperador"] = false;
                    }
                    //Guardar o display para utilização futura
                    Session["primeiroOperando"] = ecra;
                    Session["iniciarOperando"] = true;
                    break;
                //Apagar o valor dentro do ecrã
                case "C":
                    ecra = "0";
                    Session["primeiraVezOperador"] = true;
                    Session["iniciarOperando"] = false;
                    break;
            }
            //prepara o conteúdo a aparecer no VISOR da View
            ViewBag.Ecra = ecra;


            return View();
        }
    }
}