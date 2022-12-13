var globaUserProperties = new Object();
function GetglobalDomain() {
   
    var base_url = window.location;
    globaUserProperties.domain = base_url.origin;// + "/" + window.location.pathname.split('/')[1];
    return globaUserProperties.domain;
}