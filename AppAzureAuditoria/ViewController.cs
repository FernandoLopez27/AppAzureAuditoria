using CoreAnimation;
using CoreGraphics;
using Foundation;
using JavaScriptCore;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using UIKit;
using System.Json;
using Newtonsoft.Json;


namespace AppAzureAuditoria
{
    public partial class ViewController : UIViewController
    {
        public List<Datos> Lista = new List<Datos>();
        UIAlertController Alerta;
        public ViewController (IntPtr handle) : base (handle)
        {
        }

        public override async void ViewDidLoad ()
        {
            base.ViewDidLoad ();
            CAGradientLayer gradiente = new CAGradientLayer();
            gradiente.Frame = View.Bounds;
            gradiente.Colors = new CGColor[]
                           {
                UIColor.Red.CGColor,
                UIColor.Blue.CGColor
            };
            gradiente.Locations = new NSNumber[]
            {
                new NSNumber(0.0),
                new NSNumber(1.0)
            };
            try
            {
                var REST = "https://apisqlserverauditoria.azurewebsites.net/Principal/ConsultarTodo";
                JsonValue json = await GetData(REST);
                Transform(json);
            }
            catch (Exception ex)
            {

            }
            LlenarTabla();
            _Tabla.RowHeight = 200;
            this.View.Layer.InsertSublayer(gradiente, 0);
        }

        public async Task<JsonValue> GetData(string REST)
        {
            var request = (HttpWebRequest)WebRequest.Create
                                         (new Uri(REST));
            request.ContentType = "application/json";
            request.Method = "GET";
            using (WebResponse response = await
                request.GetResponseAsync())
            {
                using (System.IO.Stream stream =
                    response.GetResponseStream())
                {
                    var jsondoc = await Task.Run(() =>
                                        JsonValue.Load(stream));
                    return jsondoc;
                }
            }
        }

        public void Transform(JsonValue json)
        {
            try
            {
                var data = JsonConvert.DeserializeObject
                    <List<Datos>>(json.ToString());
                foreach (var fila in data)
                {
                    Lista.Add(new Datos
                    {
                        Auditor = fila.Auditor,
                        Empresa = fila.Empresa,
                        Resultado = fila.Resultado,
                        Imagen = fila.Imagen,
                        ImagenFondo = fila.ImagenFondo,
                        Latitud = fila.Latitud,
                        Longitud = fila.Longitud
                    });
                }
            }
            catch (System.Exception ex)
            {

            }
        }

        public void LlenarTabla()
        {
            //Lista.Clear();
            _Tabla.Source = null;
            _Tabla.Source = new OrigenTabla(Lista, this);
            _Tabla.ReloadData();
        }

        public void MessageBox(string titulo, string mensaje)
        {
            Alerta = UIAlertController.Create(titulo, mensaje,
                                              UIAlertControllerStyle.Alert);
            Alerta.AddAction(UIAlertAction.Create("Aceptar",
                                                  UIAlertActionStyle.Default,
                                                  null));
            PresentViewController(Alerta, true, null);
        }
    }

    public class OrigenTabla : UITableViewSource
    {
        public string Auditor, Empresa, Resultado, Imagen, ImagenFondo, Latitud, Longitud;
        List<Datos> ElementosTabla;
        string IDCelda = "Celda";
        UIViewController Controlador;
        public OrigenTabla(List<Datos> elementos,
                           UIViewController controlador)
        {
            ElementosTabla = elementos;
            Controlador = controlador;
        }
        public override UITableViewCell GetCell(UITableView tableView,
                                                NSIndexPath indexPath)
        {
            var celda = (CeldaController)tableView.DequeueReusableCell("IDCelda", indexPath);
            var asist = ElementosTabla[indexPath.Row];
            celda.ActualizarCelda(asist);
            return celda;
        }
        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return ElementosTabla.Count;
        }
        public override void RowSelected(UITableView tableView,
                                 NSIndexPath indexPath)
        {
            Auditor = ElementosTabla[indexPath.Row].Auditor;
            Empresa = ElementosTabla[indexPath.Row].Empresa;
            Resultado = ElementosTabla[indexPath.Row].Resultado;
            Imagen = ElementosTabla[indexPath.Row].Imagen;
            ImagenFondo = ElementosTabla[indexPath.Row].ImagenFondo;
            Latitud = ElementosTabla[indexPath.Row].Latitud;
            Longitud = ElementosTabla[indexPath.Row].Longitud;
            var detalle = Controlador.Storyboard.InstantiateViewController
                ("DetalleController") as DetalleController;
            detalle.Auditor = Auditor;
            detalle.Empresa = Empresa;
            detalle.Resultado = Resultado;
            detalle.ImagenS = Imagen;
            detalle.Imagenfondo = ImagenFondo;
            detalle.Latitud = double.Parse(Latitud.ToString());
            detalle.Longitud = double.Parse(Longitud.ToString());
            Controlador.PresentViewControllerAsync(detalle, true);
        }
    }
    public class Datos
    {
        public int Id { get; set; }
        public string Auditor { get; set; }
        public string Empresa { get; set; }
        public string Resultado { get; set; }
        public string Imagen { get; set; }
        public string ImagenFondo { get; set; }
        public string Latitud { get; set; }
        public string Longitud { get; set; }
    }

}

