const baseUrl = "https://localhost:7166";

const axiosClient = axios.create({
    baseURL: baseUrl, 
    headers: { "RequestVerificationToken": $('input[name="__RequestVerificationToken"]').val() },    
});  

function get(url) {       
    return getRequest(axiosClient.get(url)); 
} 

function post(url, data) { 
    return getRequest(axiosClient.post(url, data)); 
} 

function deletion(url) {  
    return getRequest(axiosClient.delete(url)); 
}

function getRequest(request) {

    return request
        .then(result => { console.log(result); return result; })
        .catch(error => { console.error(error); throw error; });
}