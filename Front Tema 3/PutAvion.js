const URL = "https://localhost:7105/api/Aviones/PutAviones";

function login(event) {
  event.preventDefault(); // Evitar el envío del formulario

  let modelo = document.getElementById('txtModelo');
  let cantMotores = document.getElementById('txtCantMotores');
  let cantAsiento = document.getElementById('txtCantAsientos');
  let IdAv = document.getElementById('txtid');
  let Marca = document.getElementById('txtFabricante');
  let datosVarios = document.getElementById('txtDatosVarios');

  let mod = modelo.value;
  let motores = cantMotores.value;
  let asiento = cantAsiento.value;
  let idAvion=IdAv.value;
  let marc=Marca.value;
  let datos= datosVarios.value;

  console.log(mod, motores, asiento,idAvion,asiento,marc,datos);
  if (mod === "") {
    alert('El campo nombreUsuario es obligatorio');
    return;
  }
  if (motores === "") {
    alert('El campo password es obligatorio');
    return;
  }
  if (asiento === "") {
    alert('El campo Rol es obligatorio');
    return;
  }
  if (idAvion === "") {
    alert('El campo Rol es obligatorio');
    return;
  }
  if (marc === "") {
    alert('El campo Rol es obligatorio');
    return;
  }
  if (datos === "") {
    alert('El campo Rol es obligatorio');
    return;
  }

  const request = {
    id: parseInt(idAvion),
    idFabricante:parseInt(1),
    cantidadAsientos: parseInt(asiento),
    modelo:mod,
    cantidadMotores:parseInt(motores),
    datosVarios:datos,// Convertir a número entero
    marca: "Boeing"
  };
  console.log(request);
  fetch(URL, {
    body: JSON.stringify(request),
    method: 'put',
    headers: {
      'Content-Type': 'application/json'
    }
  })
    .then(response => response.json())
    .then(data => {
      if (data.ok) {
        alert('Se generó la Actualizacion correctamente');
      } else {
        alert(data.mensajeError);
      }
    })
    .catch(err => {
      console.error('Error en la solicitud:', err);
      alert('Ocurrió un error al realizar la solicitud. Por favor, intenta nuevamente.');
    });
}

document.getElementById('btnActualizar').addEventListener('click', login);
