const URL = "https://localhost:7105/api/Usuarios/PostUsuarios";

function login(event) {
  event.preventDefault(); 
  
  let usuario=document.getElementById('txtUsuario');
  let password=document.getElementById('txtPass');

  let rol=0;
  if(document.getElementById('txtAdministrador').checked)
    {
        let roladmin =document.getElementById('txtAdministrador');
        rol = roladmin.value;
    }
    else
    {
        if(document.getElementById('txtWeb').checked)
        {
            let rolweb =document.getElementById('txtWeb');
            rol = rolweb.value;
        }
        if(document.getElementById("txtMobile").checked)
        {
            let rolMobile=document.getElementById("txtMobile")
            rol = rolMobile.value;
        }
    }


  let nombre = usuario.value;
  let pass = password.value;

  console.log(nombre, pass, rol);
  console.log(rol);
  if (nombre === "") {
    alert('El campo USUARIO es obligatorio');
    return;
  }
  if (pass === "") {
    alert('El PASSWORD es obligatorio');
    return;
  }
  if (rol===0) {
    alert('El ROL es obligatorio');
    return;
  }

  const request = {
    nombreUsuario: nombre,
    password: pass,
    idRol: parseInt(rol), 
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

document.getElementById('btnIngresar').addEventListener('click', login);
