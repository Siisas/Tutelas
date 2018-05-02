using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Datos.Modelo
{
    /// <summary>
    /// Clase de la capa de datos para la entidad de Usuarios
    /// </summary>
    public class UsuariosModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Id { get; set; }
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get; set; }
        /// <summary>
        /// Gets or sets the email confirmed.
        /// </summary>
        /// <value>
        /// The email confirmed.
        /// </value>
        public decimal EmailConfirmed { get; set; }
        /// <summary>
        /// Gets or sets the password hash.
        /// </summary>
        /// <value>
        /// The password hash.
        /// </value>
        public string PasswordHash { get; set; }
        /// <summary>
        /// Gets or sets the security stamp.
        /// </summary>
        /// <value>
        /// The security stamp.
        /// </value>
        public string SecurityStamp { get; set; }
        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        /// <value>
        /// The phone number.
        /// </value>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// Gets or sets the phone number confirmed.
        /// </summary>
        /// <value>
        /// The phone number confirmed.
        /// </value>
        public decimal PhoneNumberConfirmed { get; set; }
        /// <summary>
        /// Gets or sets the two factor enabled.
        /// </summary>
        /// <value>
        /// The two factor enabled.
        /// </value>
        public decimal TwoFactorEnabled { get; set; }
        /// <summary>
        /// Gets or sets the lockout end date UTC.
        /// </summary>
        /// <value>
        /// The lockout end date UTC.
        /// </value>
        public Nullable<System.DateTime> LockoutEndDateUtc { get; set; }
        /// <summary>
        /// Gets or sets the lockout enabled.
        /// </summary>
        /// <value>
        /// The lockout enabled.
        /// </value>
        public decimal LockoutEnabled { get; set; }
        /// <summary>
        /// Gets or sets the access failed count.
        /// </summary>
        /// <value>
        /// The access failed count.
        /// </value>
        public decimal AccessFailedCount { get; set; }
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        public string UserName { get; set; }
        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public string first_name { get; set; }
        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        public string last_name { get; set; }
        /// <summary>
        /// Gets or sets the nombre.
        /// </summary>
        /// <value>
        /// The nombre.
        /// </value>
        public string Nombre { get; set; }
        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>
        /// The created date.
        /// </value>
        public System.DateTime created_date { get; set; }
        /// <summary>
        /// Gets or sets the last activity.
        /// </summary>
        /// <value>
        /// The last activity.
        /// </value>
        public System.DateTime last_activity { get; set; }
        /// <summary>
        /// Gets or sets the time stamp.
        /// </summary>
        /// <value>
        /// The time stamp.
        /// </value>
        public System.DateTime time_stamp { get; set; }
    }

    /// <summary>
    /// Clase de la capa de datos para el esquema de seguridad (Membership)
    /// </summary>
    public class MembershipModel
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public string User_Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the user nick.
        /// </summary>
        /// <value>
        /// The name of the user nick.
        /// </value>
        [Required]
        [Display(Name = "Usuario")]
        public string User_NickName { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        [Required]
        [Display(Name = "Nombres")]
        public string User_Name { get; set; }

        /// <summary>
        /// Gets or sets the last name of the user.
        /// </summary>
        /// <value>
        /// The last name of the user.
        /// </value>
        [Required]
        [Display(Name = "Apellidos")]
        public string User_LastName { get; set; }

        /// <summary>
        /// Gets or sets the user mail.
        /// </summary>
        /// <value>
        /// The user mail.
        /// </value>
        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Introduzca un correo válido.")]
        public string User_Mail { get; set; }

        /// <summary>
        /// Gets or sets the user seccional.
        /// </summary>
        /// <value>
        /// The user seccional.
        /// </value>
        [Required]
        [Display(Name = "Despacho")]
        public string UserDespacho { get; set; }

        /// <summary>
        /// Gets or sets the user role.
        /// </summary>
        /// <value>
        /// The user role.
        /// </value>
        [Required]
        [Display(Name = "Roles")]
        public string User_Role { get; set; }
        /// <summary>
        /// Gets or sets the user enable.
        /// </summary>
        /// <value>
        /// The user enable.
        /// </value>
        public string User_Enable { get; set; }
    }

    /// <summary>
    /// Clase de la capa de datos para la entidad de Roles (Membership)
    /// </summary>
    public class RoleModel
    {
        /// <summary>
        /// Gets or sets the name of the role.
        /// </summary>
        /// <value>
        /// The name of the role.
        /// </value>
        public string Role_Name { get; set; }
        /// <summary>
        /// Gets or sets the role identifier.
        /// </summary>
        /// <value>
        /// The role identifier.
        /// </value>
        public string Role_Id { get; set; }
    }

    /// <summary>
    /// Clase de la capa de datos para la entidad de Permisos (Membership)
    /// </summary>
    public class PermissionModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Id { get; set; }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the opciones.
        /// </summary>
        /// <value>
        /// The opciones.
        /// </value>
        public string Opciones { get; set; }
        /// <summary>
        /// Gets or sets the permisos.
        /// </summary>
        /// <value>
        /// The permisos.
        /// </value>
        public string Permisos { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="PermissionModel"/> is enable.
        /// </summary>
        /// <value>
        ///   <c>true</c> if enable; otherwise, <c>false</c>.
        /// </value>
        public bool Enable { get; set; }
        /// <summary>
        /// Gets or sets the orden.
        /// </summary>
        /// <value>
        /// The orden.
        /// </value>
        public int Orden { get; set; }
    }
}
