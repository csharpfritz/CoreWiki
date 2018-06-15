// Write your Javascript code.
(function () {

	var reformatTimeStamps = function () {

		var timeStamps = document.querySelectorAll(".timeStampValue");
		for (var ts of timeStamps) {

			var thisTimeStamp = ts.getAttribute("data-value");
			var date = new Date(thisTimeStamp);
			ts.textContent = moment(date).format('LLL');

		}

	}

	reformatTimeStamps();

})();

//Prevent duplicate submit

$("form").submit(function () {

	if (this.id == "externalLogin") return true;


	if ($(this).valid()) {
		$(this).find(':submit').attr('disabled', 'disabled');
	}
});
