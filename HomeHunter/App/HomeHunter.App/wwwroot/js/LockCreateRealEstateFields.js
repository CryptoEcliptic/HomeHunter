document.getElementById('realEstateType').onchange = function () {
    if (this.value == 'Парцел') {
    
        document.getElementById("FloorNumber").disabled = true;
        document.getElementById("Year").disabled = true;
        document.getElementById("BuildingTotalFloors").disabled = true;
        document.getElementById("heatingSystem").disabled = true;
        document.getElementById("heatingSystem").style.display = "none";
        document.getElementById("buildingType").disabled = true;
        document.getElementById("buildingType").style.display = "none";
        document.getElementById("ParkingPlace").disabled = true;
        document.getElementById("Yard").disabled = true;
        document.getElementById("MetroNearBy").disabled = true;
        document.getElementById("balcony").disabled = true;
        document.getElementById("CellingOrBasement").disabled = true;

    }

    else {
        document.getElementById("FloorNumber").disabled = false;
        document.getElementById("Year").disabled = false;
        document.getElementById("BuildingTotalFloors").disabled = false;
        document.getElementById("heatingSystem").disabled = false;
        document.getElementById("heatingSystem").style.display = "inherit";
        document.getElementById("buildingType").disabled = true;
        document.getElementById("buildingType").style.display = "inherit";
        document.getElementById("ParkingPlace").disabled = false;
        document.getElementById("Yard").disabled = false;
        document.getElementById("MetroNearBy").disabled = false;
        document.getElementById("balcony").disabled = false;
        document.getElementById("CellingOrBasement").disabled = false;
    }
}