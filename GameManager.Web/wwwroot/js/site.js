$(document).ready(function () {
    $("#txtTitleFilter").keyup(function () {

        var token = $('input[name="__RequestVerificationToken"]', $('#logoutForm')).val();
        var data = { gameTitle: $("#txtTitleFilter").val() };
        var dataAntiforgeryToken = $.extend(data, { '__RequestVerificationToken': token });

        $.ajax({
            type: "POST",
            url: "/Game/Search",
            data: dataAntiforgeryToken,
            success: function (data) {
                var html = "";

                $.each(data, function (key, value) {
                    html += "<tr>";

                    html += '<td class="col-md-7">';
                    html += value.title;
                    html += '</td><td class="col-md-1" style="text-align:center"><input class="check-box" disabled="disabled" type="checkbox"';
                    if (value.borrowed) {
                        html += ' checked="checked"'
                    }                    
                    html += '></td><td class="col-md-4" style="text-align:center">';
                    html += '<a href="/Game/Edit/' + value.id + '">Editar</a> | ';
                    html += '<a href="/Game/Details/' + value.id + '">Detalhes</a> | ';
                    html += '<a href="#" onclick="javascript:DeleteGame(' + "'" + value.id + "'" + ', ' + "'" + value.title + "'" +')">Excluir</a>';

                    html += "</td>"

                    html += "</tr>";
                });                

                $("#gridGames").html(html);
            },
            error: function () {

            }
        });
    });
});

function DeleteGame(gameId, gameTitle) {
    if (confirm('Deseja excluir o jogo ' + gameTitle + '?')) {
        window.location = '/Game/Delete/' + gameId;
    }
}