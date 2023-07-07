const URL = "https://localhost:7105/api/Aviones/PostAviones";

function login(event) {
  event.preventDefault(); // Evitar el envío del formulario

  let modelo = document.getElementById('txtModelo');
  let cantMotores = document.getElementById('txtCantMotores');
  let cantAsiento = document.getElementById('txtCantAsientos');
  let Marca = document.getElementById('txtFabricante');
  let datosVarios = document.getElementById('txtDatosVarios');

  let mod = modelo.value;
  let motores = cantMotores.value;
  let asiento = cantAsiento.value;
  let marc=Marca.value;
  let datos= datosVarios.value;

  console.log(mod, motores, asiento,asiento,marc,datos);
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
  if (marc === "") {
    alert('El campo Rol es obligatorio');
    return;
  }
  if (datos === "") {
    alert('El campo Rol es obligatorio');
    return;
  }

  const request = {
    idFabricante:parseInt(marc),
    cantidadAsientos: parseInt(asiento),
    modelo:mod,
    cantidadMotores:parseInt(motores),
    datosVarios:datos,
    marca:"Boeing"
  };
  console.log(request);
  fetch(URL, {
    body: JSON.stringify(request),
    method: 'Post',
    headers: {
      'Content-Type': 'application/json'
    }
  })
    .then(response => response.json())
    .then(data => {
      if (data.ok) {
        alert('Se creo correctamente el avion');
      } else {
        alert(data.mensajeError);
      }
    })
    .catch(err => {
      console.error('Error en la solicitud:', err);
      alert('Ocurrió un error al realizar la solicitud. Por favor, intenta nuevamente.');
    });
}

document.getElementById('BtnInsertar').addEventListener('click', login);
