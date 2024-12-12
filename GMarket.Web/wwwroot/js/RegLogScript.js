document.addEventListener('DOMContentLoaded', () => {
    
    function hiddenOpen_Closelick(container){
        let x = document.querySelector(container);
        if(x.style.display === "none"){
            x.style.display = "grid";
        }else{
            x.style.display = "none";
        }
    }
    
    document.getElementById("click-to-hide").addEventListener("click", function() {
        hiddenOpen_Closelick(".container-login-registration");
    });
    document.querySelector(".overlay").addEventListener("click", function() {
        hiddenOpen_Closelick(".container-login-registration");
        });
    /*document.getElementById("side-menu-button-click-to-hide").addEventListener("click", function() {
        hiddenOpen_Closelick(".container-login-registration");
    });*/
    
    document.querySelector(".button_confirm_close").addEventListener("click", function() {
        hiddenOpen_Closelick(".confirm-email-container");
    });
    
    
    const signInBtn = document.querySelector(".signin-btn");
    const signUpBtn = document.querySelector(".signup-btn");
    const formBox = document.querySelector(".form-box");
    const block = document.querySelector(".block");
    
    if(signInBtn && signUpBtn){
        signUpBtn.addEventListener("click", function (){
            formBox.classList.add("active");
            block.classList.add('active');
        });
        
        signInBtn.addEventListener("click", function (){
            formBox.classList.remove("active");
            block.classList.remove('active');
        })
    }
    
    function sendRequest(method, url, body){
        const headers = {
            'Content-Type': 'application/json',
        }
        return fetch(url, {
            method: method,
            body: JSON.stringify(body),
            headers: headers,
        }).then(response => {
            if(!response.ok){
                return response.json().then(errorData => {
                    throw errorData;
                })
            }
            return response.text();
        })
    }
    
    function displayErrors(errors, errorContainer){
        errorContainer.innerHTML = '';
        errors.forEach(error => {
            const errorMessage = document.createElement("div");
            errorMessage.classList.add('error');
            errorMessage.textContent = error;
            errorContainer.appendChild(errorMessage);
        })
    }
    
    function cleaningAndClosingForm(form, errorContainer){
        errorContainer.innerHTML = '';
        for(const key in form){
            if(form.hasOwnProperty(key)){
                form[key].value= '';
            }
        }
        hiddenOpen_Closelick(".container-login-registration");
    }
    
    const form_btn_signin = document.querySelector(".form_btn_signin");
    const form_btn_signup = document.querySelector(".form_btn_signup");
    
    if(form_btn_signin){
        form_btn_signin.addEventListener('click', function(){
            
           const requestUrl = 'Home/Login'; 
           
           const errorContainer = document.getElementById('error-messages-signin')
           
           const form = {
               email: document.getElementById('signin_email'),
               password: document.getElementById('signin_password')
           }
           
           const body = {
               email: form.email.value,
               password: form.password.value,
           }
           
           sendRequest("POST", requestUrl, body)
               .then(data=>{
                   cleaningAndClosingForm(form,errorContainer);
                   window.location.reload();
                   console.log(errorContainer);
               })
               .catch(err=>{
                   displayErrors(err, errorContainer);
                   console.log(err);
               })
        });
    }

    if(form_btn_signup){
        form_btn_signup.addEventListener('click', function(){
            const requestUrl = 'Home/Register';

            const errorContainer = document.getElementById('error-messages-signup')

            const form = {
                email: document.getElementById('signup_email'),
                login: document.getElementById('signup_login'),
                password: document.getElementById('signup_password'),
                ConfirmPassword: document.getElementById('signup_confirm_password'),
            }

            const body = {
                email: form.email.value,
                login: form.login.value,
                password: form.password.value,
                ConfirmPassword: form.ConfirmPassword.value,
            }

            sendRequest("POST", requestUrl, body)
                .then(data=>{
                    cleaningAndClosingForm(form,errorContainer);
                    console.log(errorContainer);
                    hiddenOpen_Closelick(".confirm-email-container")
                    responsejson = JSON.parse(data);
                    
                    confirmEmail(responsejson);
                })
                .catch(err=>{
                    displayErrors(err, errorContainer);

                    console.log(err);
                })
        });
    }
    
    function confirmEmail(data){
        document.querySelector(".send_confirm").addEventListener("click", function (){
            data.codeConfirm = document.getElementById("code_confirm").value;
            const requestUrl = '/Home/ConfirmEmail';
            const errorContainer = document.getElementById('error-messages-signup')

            sendRequest("POST", requestUrl, data)
                .then(dataResponse=>{
                    console.log("Code: ", dataResponse);
                    
                    hiddenOpen_Closelick(".confirm-email-container");
                    
                    window.location.reload();
                })
                .catch(err=>{
                    displayErrors(err, errorContainer)
                    console.log(err);
                });
        })
    }
    
    
    const google = document.querySelectorAll('.google');
    
    if(google){
        google.forEach(btn =>{
            btn.addEventListener('click', function(){
                window.location.href = `/Home/AuthenticationGoogle?returnUrl=${encodeURIComponent(window.location.href)}`;
            })
        })
    }
    
})