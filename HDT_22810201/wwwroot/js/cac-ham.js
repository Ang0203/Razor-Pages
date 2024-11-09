function xacNhanMe() {
	var tar = document.getElementById("xacnhan");
	tar.value = "xacnhan";
	document.getElementById("zform").submit();
}
function sapXep(id, leng) {
	var tar = document.getElementById("ds-" + id);
	switch (tar.value) {
		case "0":
			var collection = document.getElementsByClassName("ds-sx");
			for (var i = 0; i < leng; i++) {
				collection[i].value = "0";
			}
			tar.value = "tang";
			break;
		case "tang":
			tar.value = "giam";
			break;
		case "giam":
			tar.value = "tang";
			break;
	}
	document.getElementById("zform").submit();
}
function chonLoaiCty(id) {
	var val = document.getElementById(id).value;
	var tar = document.getElementById(id + "-inp");

	if (val == "") {
		tar.setAttribute("required", "required");
		tar.removeAttribute("readonly", "");
		tar.style.backgroundColor = "white";
		tar.value = "";
	} else {
		tar.removeAttribute("required");
		tar.setAttribute("readonly", "");
		tar.style.backgroundColor = "#d3d3d3";
		tar.value = val;
	}
}
function taoNgay(date) {
	var day = ("0" + date.split("/")[0]).slice(-2);
	var month = ("0" + date.split("/")[1]).slice(-2);
	var year = ("0" + date.split("/")[2]).slice(-4);
	return year + "-" + month + "-" + day;
}
function gioiHanNgay(leng) {
	var date = new Date()
		.toLocaleString("vi-VN")
		.slice(9);
	var next_date = new Date(new Date().getTime() + (24 * 60 * 60 * 1000))
		.toLocaleString("vi-VN")
		.slice(9);

	var nsx = document.getElementsByClassName("nsx");
	var hd = document.getElementsByClassName("hd");
	for (var i = 0; i < leng; i++) {
		nsx[i].setAttribute("max", taoNgay(date));
		hd[i].setAttribute("min", taoNgay(next_date));
	}
}
function chonTim(id) {
	var val = document.getElementById(id).value;
	if (val == "0") {
		document.getElementById(id + "-cxts").setAttribute("hidden", "hidden");
		document.getElementById(id + "-tk").setAttribute("hidden", "hidden");

		document.getElementById(id + "-cxts1").removeAttribute("required");
		document.getElementById(id + "-tk1").removeAttribute("required");
		document.getElementById(id + "-tk2").removeAttribute("required");
	} else if (val == "tk") {
		document.getElementById(id + "-tk").removeAttribute("hidden");
		document.getElementById(id + "-tk1").setAttribute("required", "required");
		document.getElementById(id + "-tk2").setAttribute("required", "required");

		document.getElementById(id + "-cxts").setAttribute("hidden", "hidden");
		document.getElementById(id + "-cxts1").removeAttribute("required");
	} else{
		document.getElementById(id + "-cxts").removeAttribute("hidden");
		document.getElementById(id + "-cxts1").setAttribute("required", "required");

		document.getElementById(id + "-tk").setAttribute("hidden", "hidden");
		document.getElementById(id + "-tk1").removeAttribute("required");
		document.getElementById(id + "-tk2").removeAttribute("required");
	}
}