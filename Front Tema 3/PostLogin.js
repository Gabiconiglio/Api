const URL = "https://localhost:7105/api/Usuarios/PostUsuarios";

function login(event) {
  event.preventDefault(); // Evitar el envío del formulario

  let nombreUsuario = document.getElementById('txtUsuario');
  let password = document.getElementById('txtPassword');
  let rol = document.getElementById('valor');

  let nombre = nombreUsuario.value;
  let pass = password.value;
  let roles = rol.value;

  console.log(nombre, pass, roles);
  if (nombre === "") {
    alert('El campo nombreUsuario es obligatorio');
    return;
  }
  if (pass === "") {
    alert('El campo password es obligatorio');
    return;
  }
  if (roles === "") {
    alert('El campo Rol es obligatorio');
    return;
  }

  const request = {
    nombreUsuario: nombre,
    password: pass,
    idRol: parseInt(roles), // Convertir a número entero
  };
  console.log(request);
  fetch(URL, {
    body: JSON.stringify(request),
    method: 'post',
    headers: {
      'Content-Type': 'application/json'
    }
  })
    .then(response => response.json())
    .then(data => {
      if (data.ok) {
        alert('Se generó el login correctamente');
      } else {
        alert(data.mensajeError);
      }
    })
    .catch(err => {
      console.error('Error en la solicitud:', err);
      alert('Ocurrió un error al realizar la solicitud. Por favor, intenta nuevamente.');
    });
}

document.getElementById('btn').addEventListener('click', login);
