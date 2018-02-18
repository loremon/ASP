function incLikeCount(reviewId) {
    var addition = String(reviewId);
    $.post({
        'url': '/Review/IncLikeCount',
        'data': {
            addition: addition
        },
        'success': function (likeCount) {
            $('#likes_' + addition).html('' + likeCount);
        },
        'error': function (_, error) {
            alert('error: ' + error);
        }
    });
}

function reportReview(reviewId) {
    var addition = String(reviewId);
    if (confirm('Вы действительно хотите пометить этот отзыв как оскорбительный?')) {
        var reason = prompt('Укажите причину, пожалуйста');
        if (reason != null && reason != '') {
            $.post({
                'url': '/Review/ReportReview',
                'data': {
                    addition: addition,
                    text: reason
                },
                'success': function (code) {
                    if (code == 0) {
                        $('#review_' + addition).css('opacity', 0.4);
                        alert('успешно');
                    } else {
                        alert('произошла ошибка, попробуйте позднее');
                    }
                },
                'error': function (_, error) {
                    alert('error: ' + error);
                }
            });
        }
    }
}

function tryAddReview() {
    var badWords = getBadWords();
    var text = '' + $("#Text").val();
    var newText = text;
    for (var word in badWords) {
        newText = newText.split(word).join(badWords[word]);
    }
    if (text !== newText) {
        if (confirm('Были найдены нехорошие слова. Хотите заменить их на звёздочки, или исправите сами?')) {
            $('#Text').val(newText);
        } else {
            return false;
        }
    }
    return true;
}

function getBadWords() {
    return {
        'отстой': '******',
        'отвратительно': '*************',
        'скука': '*****',
        'глупость': '********',
        'ужасно': '******',
        'невыносимо': '**********'
    };
}

function prev() {
    var pageNumber = parseInt($('#pn').val()) - 1;
    var booksOnPage = $('#bop').val();
    window.location.href = '/Book/Books/' + pageNumber + 'P' + booksOnPage;
}

function next() {
    var pageNumber = parseInt($('#pn').val()) + 1;
    var booksOnPage = $('#bop').val();
    window.location.href = '/Book/Books/' + pageNumber + 'P' + booksOnPage;
}

function reloadBooks() {
    var pageNumber = $('#pn').val();
    var booksOnPage = $('#bop').val();
    window.location.href = '/Book/Books/' + pageNumber + 'P' + booksOnPage;
}

$(document).foundation();

$('input').each(function (index, elem) {
    $(elem).attr('autocomplete', 'off');
});