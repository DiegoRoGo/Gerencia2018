$(document).ready(function(){


  // Custom method to validate username
  $.validator.addMethod("usernameRegex", function(value, element) {
    return this.optional(element) || /^[a-zA-Z0-9]*$/i.test(value);
  }, "Username must contain only letters, numbers");

  $(".next").click(function(){
    var form = $("#myform");

    form.validate({
      errorElement: 'span',
      errorClass: 'help-block',
      highlight: function(element, errorClass, validClass) {
        $(element).closest('.form-group').addClass("has-error");
      },
      unhighlight: function(element, errorClass, validClass) {
        $(element).closest('.form-group').removeClass("has-error");
      },
      rules: {
        abono: {
          required: true,
          number: true,
        },
      },
      messages: {
        abono: {
          required: "Campo requerido",
          number: "Debe ingresar solo números",
        },
      }
    });

    if (form.valid() === true){
      if ($('#realizar_pagos').is(":visible")){
          current_fs = $('#realizar_pagos');
          next_fs = $('#saldos_cuenta_corriente');
      }else if($('#saldos_cuenta_corriente').is(":visible")){
        current_fs = $('#saldos_cuenta_corriente');
        next_fs = $('#abonar_monto');
      }else if($('#abonar_monto').is(":visible")){
        //current_fs = $('#abonar_monto');
      }
      
        next_fs.show(); 
        current_fs.hide();

    }

  });

  $('.previous').click(function(){
    if($('#saldos_cuenta_corriente').is(":visible")){
      current_fs = $('#saldos_cuenta_corriente');
      next_fs = $('#realizar_pagos');
    }else if ($('#abonar_monto').is(":visible")){
      current_fs = $('#abonar_monto');
      next_fs = $('#saldos_cuenta_corriente');
    }

    next_fs.show(); 
    current_fs.hide();
  });



});
