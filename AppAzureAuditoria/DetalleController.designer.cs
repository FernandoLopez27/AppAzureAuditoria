// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace AppAzureAuditoria
{
	[Register ("DetalleController")]
	partial class DetalleController
	{
		[Outlet]
		UIKit.UIImageView _Imagen { get; set; }

		[Outlet]
		UIKit.UIImageView _ImagenFondo { get; set; }

		[Outlet]
		UIKit.UILabel _lblAuditor { get; set; }

		[Outlet]
		UIKit.UILabel _lblEmpresa { get; set; }

		[Outlet]
		UIKit.UILabel _lblResultado { get; set; }

		[Outlet]
		MapKit.MKMapView _Mapa { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (_Imagen != null) {
				_Imagen.Dispose ();
				_Imagen = null;
			}

			if (_ImagenFondo != null) {
				_ImagenFondo.Dispose ();
				_ImagenFondo = null;
			}

			if (_lblAuditor != null) {
				_lblAuditor.Dispose ();
				_lblAuditor = null;
			}

			if (_lblEmpresa != null) {
				_lblEmpresa.Dispose ();
				_lblEmpresa = null;
			}

			if (_lblResultado != null) {
				_lblResultado.Dispose ();
				_lblResultado = null;
			}

			if (_Mapa != null) {
				_Mapa.Dispose ();
				_Mapa = null;
			}
		}
	}
}
