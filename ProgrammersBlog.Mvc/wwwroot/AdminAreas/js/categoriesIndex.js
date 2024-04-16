$(document).ready(function () {

    /* DataTables start here */

    const dataTable = $('#categoriesTable').DataTable({
        dom:
            "<'row'<'col-sm-3'l><'col-sm-6 text-center'B><'col-sm-3'f>>" +
            "<'row'<'col-sm-12'tr>>" +
            "<'row'<'col-sm-9'i><'col-sm-3'p>>",
        "order": [[6, "desc"]],
        buttons: [
            {
                text: 'Ekle',
                attr: {
                    id: "btnAdd",
                },
                className: 'btn btn-success',
                action: function (e, dt, node, config) {

                }
            },
            {
                text: 'Yenile',
                className: 'btn btn-info',
                action: function (e, dt, node, config) {
                    $.ajax({
                        type: 'GET',
                        url: '/Admin/Categories/GetAllCategories/',
                        contentType: "application/json",
                        beforeSend: function () {
                            $('#categoriesTable').hide();
                            $('.spinner-border').show();
                        },
                        success: function (data) {
                            const categoryListDto = jQuery.parseJSON(data);
                            dataTable.clear();
                            console.log(categoryListDto);
                            if (categoryListDto.ResultStatus === 0) {
                                $.each(categoryListDto.Categories.$values,
                                    function (index, category) {
                                        const newTableRow = dataTable.row.add([
                                            category.Id,
                                            category.Name,
                                            category.Description,
                                            `${category.IsActive ? "Evet" : "Hayır"}`,
                                            `${category.IsDeleted ? "Evet" : "Hayır"}`,
                                            category.Note,
                                            `${convertToShortDate(category.CreatedDate)}`,
                                            category.CreatedByName,
                                            `${convertToShortDate(category.ModifiedDate)}`,
                                            category.ModifiedByName,
                                            `
                                                <button class="btn btn-primary btn-sm btn-update" data-id="${category.Id}">
                                                            <span class="fas fa-edit"></span>
                                                        </button>
                                                <button class="btn btn-danger btn-sm btn-delete" data-id="${category.Id}">
                                                            <span class="fas fa-trash"></span>
                                                        </button>
                                            `
                                        ]).node();
                                        const jqueryTableRow = $(newTableRow);
                                        jqueryTableRow.attr("name", `${category.Id}`);
                                    });
                                dataTable.draw();
                                $('.spinner-border').hide();
                                $('#categoriesTable').fadeIn(1400);
                            }
                            else {
                                toastr.error(`${categoryListDto.Message}`, 'İşlem Başarısız!');
                            }
                        },
                        error: function (err) {
                            console.log(err);
                            $('.spinner-border').hide();
                            $('#categoriesTable').fadeIn(1000);
                            toastr.error(`${err.responseText}`, 'Hata!');
                        }
                    });
                }
            }
        ],
        language: {
            "emptyTable": "Tabloda herhangi bir veri mevcut değil",
            "info": "_TOTAL_ kayıttan _START_ - _END_ arasındaki kayıtlar gösteriliyor",
            "infoEmpty": "Kayıt yok",
            "infoFiltered": "(_MAX_ kayıt içerisinden bulunan)",
            "infoThousands": ".",
            "lengthMenu": "Sayfada _MENU_ kayıt göster",
            "loadingRecords": "Yükleniyor...",
            "processing": "İşleniyor...",
            "search": "Ara:",
            "zeroRecords": "Eşleşen kayıt bulunamadı",
            "paginate": {
                "first": "İlk",
                "last": "Son",
                "next": "Sonraki",
                "previous": "Önceki"
            },
            "aria": {
                "sortAscending": ": artan sütun sıralamasını aktifleştir",
                "sortDescending": ": azalan sütun sıralamasını aktifleştir"
            },
            "select": {
                "rows": {
                    "_": "%d kayıt seçildi",
                    "1": "1 kayıt seçildi"
                },
                "cells": {
                    "1": "1 hücre seçildi",
                    "_": "%d hücre seçildi"
                },
                "columns": {
                    "1": "1 sütun seçildi",
                    "_": "%d sütun seçildi"
                }
            },
            "autoFill": {
                "cancel": "İptal",
                "fillHorizontal": "Hücreleri yatay olarak doldur",
                "fillVertical": "Hücreleri dikey olarak doldur",
                "fill": "Bütün hücreleri <i>%d<\/i> ile doldur"
            },
            "buttons": {
                "collection": "Koleksiyon <span class=\"ui-button-icon-primary ui-icon ui-icon-triangle-1-s\"><\/span>",
                "colvis": "Sütun görünürlüğü",
                "colvisRestore": "Görünürlüğü eski haline getir",
                "copySuccess": {
                    "1": "1 satır panoya kopyalandı",
                    "_": "%ds satır panoya kopyalandı"
                },
                "copyTitle": "Panoya kopyala",
                "csv": "CSV",
                "excel": "Excel",
                "pageLength": {
                    "-1": "Bütün satırları göster",
                    "_": "%d satır göster"
                },
                "pdf": "PDF",
                "print": "Yazdır",
                "copy": "Kopyala",
                "copyKeys": "Tablodaki veriyi kopyalamak için CTRL veya u2318 + C tuşlarına basınız. İptal etmek için bu mesaja tıklayın veya escape tuşuna basın.",
                "createState": "Şuanki Görünümü Kaydet",
                "removeAllStates": "Tüm Görünümleri Sil",
                "removeState": "Aktif Görünümü Sil",
                "renameState": "Aktif Görünümün Adını Değiştir",
                "savedStates": "Kaydedilmiş Görünümler",
                "stateRestore": "Görünüm -&gt; %d",
                "updateState": "Aktif Görünümün Güncelle"
            },
            "searchBuilder": {
                "add": "Koşul Ekle",
                "button": {
                    "0": "Arama Oluşturucu",
                    "_": "Arama Oluşturucu (%d)"
                },
                "condition": "Koşul",
                "conditions": {
                    "date": {
                        "after": "Sonra",
                        "before": "Önce",
                        "between": "Arasında",
                        "empty": "Boş",
                        "equals": "Eşittir",
                        "not": "Değildir",
                        "notBetween": "Dışında",
                        "notEmpty": "Dolu"
                    },
                    "number": {
                        "between": "Arasında",
                        "empty": "Boş",
                        "equals": "Eşittir",
                        "gt": "Büyüktür",
                        "gte": "Büyük eşittir",
                        "lt": "Küçüktür",
                        "lte": "Küçük eşittir",
                        "not": "Değildir",
                        "notBetween": "Dışında",
                        "notEmpty": "Dolu"
                    },
                    "string": {
                        "contains": "İçerir",
                        "empty": "Boş",
                        "endsWith": "İle biter",
                        "equals": "Eşittir",
                        "not": "Değildir",
                        "notEmpty": "Dolu",
                        "startsWith": "İle başlar",
                        "notContains": "İçermeyen",
                        "notStartsWith": "Başlamayan",
                        "notEndsWith": "Bitmeyen"
                    },
                    "array": {
                        "contains": "İçerir",
                        "empty": "Boş",
                        "equals": "Eşittir",
                        "not": "Değildir",
                        "notEmpty": "Dolu",
                        "without": "Hariç"
                    }
                },
                "data": "Veri",
                "deleteTitle": "Filtreleme kuralını silin",
                "leftTitle": "Kriteri dışarı çıkart",
                "logicAnd": "ve",
                "logicOr": "veya",
                "rightTitle": "Kriteri içeri al",
                "title": {
                    "0": "Arama Oluşturucu",
                    "_": "Arama Oluşturucu (%d)"
                },
                "value": "Değer",
                "clearAll": "Filtreleri Temizle"
            },
            "searchPanes": {
                "clearMessage": "Hepsini Temizle",
                "collapse": {
                    "0": "Arama Bölmesi",
                    "_": "Arama Bölmesi (%d)"
                },
                "count": "{total}",
                "countFiltered": "{shown}\/{total}",
                "emptyPanes": "Arama Bölmesi yok",
                "loadMessage": "Arama Bölmeleri yükleniyor ...",
                "title": "Etkin filtreler - %d",
                "showMessage": "Tümünü Göster",
                "collapseMessage": "Tümünü Gizle"
            },
            "thousands": ".",
            "datetime": {
                "amPm": [
                    "öö",
                    "ös"
                ],
                "hours": "Saat",
                "minutes": "Dakika",
                "next": "Sonraki",
                "previous": "Önceki",
                "seconds": "Saniye",
                "unknown": "Bilinmeyen",
                "weekdays": {
                    "6": "Paz",
                    "5": "Cmt",
                    "4": "Cum",
                    "3": "Per",
                    "2": "Çar",
                    "1": "Sal",
                    "0": "Pzt"
                },
                "months": {
                    "9": "Ekim",
                    "8": "Eylül",
                    "7": "Ağustos",
                    "6": "Temmuz",
                    "5": "Haziran",
                    "4": "Mayıs",
                    "3": "Nisan",
                    "2": "Mart",
                    "11": "Aralık",
                    "10": "Kasım",
                    "1": "Şubat",
                    "0": "Ocak"
                }
            },
            "decimal": ",",
            "editor": {
                "close": "Kapat",
                "create": {
                    "button": "Yeni",
                    "submit": "Kaydet",
                    "title": "Yeni kayıt oluştur"
                },
                "edit": {
                    "button": "Düzenle",
                    "submit": "Güncelle",
                    "title": "Kaydı düzenle"
                },
                "error": {
                    "system": "Bir sistem hatası oluştu (Ayrıntılı bilgi)"
                },
                "multi": {
                    "info": "Seçili kayıtlar bu alanda farklı değerler içeriyor. Seçili kayıtların hepsinde bu alana aynı değeri atamak için buraya tıklayın; aksi halde her kayıt bu alanda kendi değerini koruyacak.",
                    "noMulti": "Bu alan bir grup olarak değil ancak tekil olarak düzenlenebilir.",
                    "restore": "Değişiklikleri geri al",
                    "title": "Çoklu değer"
                },
                "remove": {
                    "button": "Sil",
                    "confirm": {
                        "_": "%d adet kaydı silmek istediğinize emin misiniz?",
                        "1": "Bu kaydı silmek istediğinizden emin misiniz?"
                    },
                    "submit": "Sil",
                    "title": "Kayıtları sil"
                }
            },
            "stateRestore": {
                "creationModal": {
                    "button": "Kaydet",
                    "columns": {
                        "search": "Kolon Araması",
                        "visible": "Kolon Görünümü"
                    },
                    "name": "Görünüm İsmi",
                    "order": "Sıralama",
                    "paging": "Sayfalama",
                    "scroller": "Kaydırma (Scrool)",
                    "search": "Arama",
                    "searchBuilder": "Arama Oluşturucu",
                    "select": "Seçimler",
                    "title": "Yeni Görünüm Oluştur",
                    "toggleLabel": "Kaydedilecek Olanlar"
                },
                "duplicateError": "Bu Görünüm Daha Önce Tanımlanmış",
                "emptyError": "Görünüm Boş Olamaz",
                "emptyStates": "Herhangi Bir Görünüm Yok",
                "removeConfirm": "Görünümü Silmek İstediğinize Eminminisiniz?",
                "removeError": "Görünüm Silinemedi",
                "removeJoiner": "ve",
                "removeSubmit": "Sil",
                "removeTitle": "Görünüm Sil",
                "renameButton": "Değiştir",
                "renameLabel": "Görünüme Yeni İsim Ver -&gt; %s:",
                "renameTitle": "Görünüm İsmini Değiştir"
            }
        }
    });

    /* DataTables ends here */

    /* Ajax GET / Getting the _CategoryAddPartial as Modal Form Starts From Here */

    $(function () {
        const url = '/Admin/Categories/Add/';
        const placeHolderDiv = $('#modalPlaceHolder');
        $('#btnAdd').click(function () {
            $.get(url).done(function (data) {
                placeHolderDiv.html(data);
                placeHolderDiv.find(".modal").modal('show');
            });
        });

        /* Ajax GET / Getting the _CategoryAddPartial as Modal Form Ends From Here */

        /* Ajax POST / Posting the _CategoryAddPartial as Modal Form Starts From Here */

        placeHolderDiv.on(
            'click',
            '#btnSave',
            function (event) {
                event.preventDefault();
                const form = $('#form-category-add');
                const actionUrl = form.attr('action');
                const dataToSend = new FormData(form.get(0));
                $.ajax({
                    url: actionUrl,
                    type: 'POST',
                    data: dataToSend,
                    processData: false,
                    contentType: false,
                    success: (function (data) {
                        console.log(data);
                        const categoryAddAjaxModel = jQuery.parseJSON(data);
                        console.log(categoryAddAjaxModel);
                        const id = categoryAddAjaxModel.CategoryDto.Category.Id;
                        const newFormBody = $('.modal-body', categoryAddAjaxModel.CategoryAddPartial);
                        placeHolderDiv.find('.modal-body').replaceWith(newFormBody);
                        const isValid = newFormBody.find('[name="IsValid"]').val() === 'True';
                        if (isValid) {
                            placeHolderDiv.find('.modal').modal('hide');
                            const newTableRow = dataTable.row.add([
                                categoryAddAjaxModel.CategoryDto.Category.Id,
                                categoryAddAjaxModel.CategoryDto.Category.Name,
                                categoryAddAjaxModel.CategoryDto.Category.Description,
                                `${categoryAddAjaxModel.CategoryDto.Category.IsActive ? "Evet" : "Hayır"}`,
                                `${categoryAddAjaxModel.CategoryDto.Category.IsDeleted ? "Evet" : "Hayır"}`,
                                categoryAddAjaxModel.CategoryDto.Category.Note,
                                `${convertToShortDate(categoryAddAjaxModel.CategoryDto.Category.CreatedDate)}`,
                                categoryAddAjaxModel.CategoryDto.Category.CreatedByName,
                                `${convertToShortDate(categoryAddAjaxModel.CategoryDto.Category.ModifiedDate)}`,
                                categoryAddAjaxModel.CategoryDto.Category.ModifiedByName,
                                `
                                                <button class="btn btn-primary btn-sm btn-update" data-id="${categoryAddAjaxModel.CategoryDto.Category.Id}">
                                                            <span class="fas fa-edit"></span>
                                                        </button>
                                                <button class="btn btn-danger btn-sm btn-delete" data-id="${categoryAddAjaxModel.CategoryDto.Category.Id}">
                                                            <span class="fas fa-trash"></span>
                                                        </button>
                                            `
                            ]);
                            const jqueryTableRow = $(newTableRow);
                            jqueryTableRow.attr("name", `${categoryAddAjaxModel.CategoryDto.Category.Id}`);
                            dataTable.row(newTableRow).draw();
                            toastr.success(`${categoryAddAjaxModel.CategoryDto.Message}`, 'Başarılı İşlem!');
                        } else {
                            let summaryText = "";
                            $('#validation-summary > ul > li').each(function () {
                                let text = $(this).text();
                                summaryText = `*${text}\n`;
                            });
                            toastr.warning(summaryText);
                        }
                    }),
                    error: function (error) {
                        console.log(error);
                        toastr.error(`${err.responseText}`, 'Hata!');

                    }
                });
            });
    });

        /* Ajax POST / Posting the FormData as CategoryAddDto ends here */

        /* Ajax POST / Deleting a Category starts from here */

        $(document).on('click',
            '.btn-delete',
            function (event) {
                event.preventDefault();
                const id = $(this).attr('data-id');
                const tableRow = $(`[name="${id}"]`);
                const categoryName = tableRow.find('td:eq(1)').text();
                Swal.fire({
                    title: 'Silmek istediğinize emin misiniz?',
                    text: `${categoryName} adlı kategori silinecektir!`,
                    icon: 'warning',
                    showCancelButton: true,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33',
                    confirmButtonText: 'Evet, silmek istiyorum.',
                    cancelButtonText: 'Hayır, silmek istemiyorum.'

                }).then((result) => {
                    if (result.isConfirmed) {
                        $.ajax({
                            type: 'POST',
                            dataType: 'json',
                            data: { categoryId: id },
                            url: '/Admin/Categories/Delete/',
                            success: function (data) {
                                const categoryDto = jQuery.parseJSON(data);
                                if (categoryDto.ResultStatus === 0) {
                                    Swal.fire(
                                        'Silindi!',
                                        `${categoryDto.Category.Name} adlı kategori başarıyla silinmiştir.`,
                                        'success'
                                    );
                                    dataTable.row(tableRow).remove().draw();
                                }
                                else {
                                    Swal.fire({
                                        icon: 'error',
                                        title: 'İşlem Başarısız!',
                                        text: `${categoryDto.Message}`
                                    });
                                }
                            },
                            error: function (err) {
                                console.log(err);
                                toastr.error(`${err.responseText}`, "Hata!");
                            }
                        });
                    }
                });
            });

        /* Ajax POST / Deleting a Category ends here */

        /* Ajax GET / Getting the _CategoryUpdatePartial as Modal Form Starts From Here */

        $(function () {
            const url = '/Admin/Categories/Update/';
            const placeHolderDiv = $('#modalPlaceHolder');
            $(document).on(
                'click',
                '.btn-update',
                function (event) {
                    event.preventDefault();
                    const id = $(this).attr('data-id');
                    $.get(url, { categoryId: id })
                        .done(function (data) {
                            placeHolderDiv.html(data);
                            placeHolderDiv.find('.modal').modal('show');
                        })
                        .fail(function () {
                            toastr.error(`${err.responseText}`, 'Hata!');
                        });
                });

            /* Ajax GET / Getting the _CategoryUpdatePartial as Modal Form ends here */

            /* Ajax POST / Updating a Category starts from here */

            placeHolderDiv.on(
                'click',
                '#btnUpdate',
                function (event) {
                    event.preventDefault();
                    const form = $('#form-category-update');
                    const actionUrl = form.attr('action');
                    const dataToSend = new FormData(form.get(0));
                    $.ajax({
                        url: actionUrl,
                        type: 'POST',
                        data: dataToSend,
                        processData: false,
                        contentType: false,
                        success: (function (data) {
                            const categoryUpdateAjaxModel = jQuery.parseJSON(data);
                            console.log(categoryUpdateAjaxModel);
                            if (categoryUpdateAjaxModel.CategoryDto != null) {
                                const id = categoryUpdateAjaxModel.CategoryDto.Category.Id;
                                const tableRow = $(`[name="${id}"]`);
                            }
                            const newFormBody = $('.modal-body', categoryUpdateAjaxModel.CategoryUpdatePartial);
                            placeHolderDiv.find('.modal-body').replaceWith(newFormBody);
                            const isValid = newFormBody.find('[name="IsValid"]').val() === 'True';
                            if (isValid) {
                                placeHolderDiv.find('.modal').modal('hide');
                                dataTable.row(tableRow).data([
                                    categoryUpdateAjaxModel.CategoryDto.Category.Id,
                                    categoryUpdateAjaxModel.CategoryDto.Category.Name,
                                    categoryUpdateAjaxModel.CategoryDto.Category.Description,
                                    `${categoryUpdateAjaxModel.CategoryDto.Category.IsActive ? "Evet" : "Hayır"}`,
                                    `${categoryUpdateAjaxModel.CategoryDto.Category.IsDeleted ? "Evet" : "Hayır"}`,
                                    categoryUpdateAjaxModel.CategoryDto.Category.Note,
                                    `${convertToShortDate(categoryUpdateAjaxModel.CategoryDto.Category.CreatedDate)}`,
                                    categoryUpdateAjaxModel.CategoryDto.Category.CreatedByName,
                                    `${convertToShortDate(categoryUpdateAjaxModel.CategoryDto.Category.ModifiedDate)}`,
                                    categoryUpdateAjaxModel.CategoryDto.Category.ModifiedByName,
                                    `
                                                <button class="btn btn-primary btn-sm btn-update" data-id="${categoryUpdateAjaxModel.CategoryDto.Category.Id}">
                                                            <span class="fas fa-edit"></span>
                                                        </button>
                                                <button class="btn btn-danger btn-sm btn-delete" data-id="${categoryUpdateAjaxModel.CategoryDto.Category.Id}">
                                                            <span class="fas fa-trash"></span>
                                                        </button>
                                            `
                                ]);
                                tableRow.attr("name", `${id}`);
                                dataTable.row(tableRow).invalidate();
                                toastr.success(`${categoryUpdateAjaxModel.CategoryDto.Message}`, "İşlem Başarılı!");
                            } else {
                                let summaryText = "";
                                $('#validation-summary > ul > li').each(function () {
                                    let text = $(this).text();
                                    summaryText = `*${text}\n`;
                                });
                                toastr.warning(summaryText);
                            }
                        }),
                        error: function (error) {
                            console.log(error);
                            toastr.error(`${err.responseText}`, 'Hata!');
                        }
                    });
                });
        });
    });