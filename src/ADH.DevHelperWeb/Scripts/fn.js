

//预览插件
//http://www.cnblogs.com/Gnepner/archive/2011/08/19/2145493.html
//http://blog.sina.com.cn/s/blog_55b703040106ir1f.html  遇到问题看这个
var viewer = $('#documentViewer').FlexPaperViewer(
    {
        config: {
            SWFFile: '/docs/readme.swf',
            Scale: 0.6,
            ZoomTransition: 'easeOut',
            ZoomTime: 0.5,
            ZoomInterval: 0.2,
            FitPageOnLoad: false,
            FitWidthOnLoad: true,
            FullScreenAsMaxWindow: false,
            ProgressiveLoading: false,
            MinZoomSize: 0.2,
            MaxZoomSize: 5,
            SearchMatchAll: false,
            InitViewMode: 'Portrait',
            RenderingOrder: 'flash',
            StartAtPage: '',

            ViewModeToolsVisible: true,
            ZoomToolsVisible: true,
            NavToolsVisible: true,
            CursorToolsVisible: true,
            SearchToolsVisible: true,
            WMode: 'window',
            localeChain: 'zh_CN'
        }
    }
);

$('#documentViewer').bind('onDocumentLoaded', function (e, totalPages) {
    //alert('文档加载完成'+totalPages);
    $FlexPaper('documentViewer').fitWidth();
});

var doc;

$(function () {
    doc = {
        treeUrl: '/Doc/Tree',
        $tree: $('#tree'),
        $myModal: $('#myModal'),
        $treeDir: $('#treeUp'),
        $btnUpload: $('#btnUpload'),
        $pnlAddSubDir: $('#pnlAddSubDir'),
        $btnAddSubDir: $('#btnAddSubDir'),
        $btnDelSubDir: $('#btnDelSubDir'),
        $txtDir: $('#txtDir'),
        $btnAddSubDirDo: $('#btnAddSubDirDo'),
        $modalError: $('#modalErrorMsg'),
        $modalErrorMsg: $('#modalErrorMsg > span'),
        $btnModalErrorClose: $('#modalErrorMsg > button')
    };

    doc.$pnlAddSubDir.hide();

    doc.$btnModalErrorClose.click(function () {
        hideModalErrorMsg();
    });

    $('[data-toggle="popover"]').popover();

    //弹窗事件
    doc.$myModal.on('hidden.bs.modal', function (e) {
        if (!/webkit/.test(navigator.userAgent.toLowerCase()))//如果不是webkit内核
            $('#documentViewer').children().show();//解决弹窗被覆盖的问题
    });

    doc.$myModal.on('show.bs.modal', function (e) {
        if (!/webkit/.test(navigator.userAgent.toLowerCase()))
            $('#documentViewer').children().hide();
    });

    //删除目录
    doc.$btnDelSubDir.click(function () {
        if (doc.$txtDir.val() === '') {
            doc.$txtDir.popover('show');
        } else {
            doc.$txtDir.popover('hide');

            $.getJSON('Doc/ModiDir', { parent: doc.$txtDir.val() }, function (data) {
                if (data.IsSuccess) {
                    doc.$txtDir.val('');
                    updateTreeDir();
                } else {
                    showModalErrorMsg(data.Msg);
                }
            });
        }
    });

    //添加目录
    doc.$btnAddSubDir.click(function () {
        if (doc.$txtDir.val() === '') {
            doc.$txtDir.popover('show');
        } else {
            doc.$txtDir.popover('hide');
            doc.$pnlAddSubDir.show();
        }
    });

    //添加目录确定
    doc.$btnAddSubDirDo.click(function () {
        var $txtSubDir = $('#txtSubDir');
        if ($txtSubDir.val() === '') {
            return;
        }

        $.getJSON('Doc/ModiDir', { parent: doc.$txtDir.val(), sub: $txtSubDir.val() }, function (data) {
            if (data.IsSuccess) {
                doc.$txtDir.val('');
                updateTreeDir();
                doc.$pnlAddSubDir.hide();
            } else {
                showModalErrorMsg(data.Msg);
            }
        });

    });
    //添加目录取消
    $('#btnAddSubDirCancel').click(function () {
        $('#txtSubDir').val('');
        doc.$pnlAddSubDir.hide();
    });


    //加载树目录
    $.getJSON(doc.treeUrl, { t: new Date().getTime(), leaf: true }, function (data) {
        //参考 http://www.htmleaf.com/jQuery/Menu-Navigation/201502141379.html
        doc.$tree.treeview({ data: data, enableLinks: false });

        //绑定事件
        doc.$tree.on('nodeSelected ', function (event, node) {
            changeDoc(node.path);
        });
    });

    doc.$btnUpload.click(function () {
        //加载目录
        updateTreeDir();
        //修改弹出属性
        doc.$myModal.modal({ backdrop: 'static', keyboard: false, show: true });
        //解决上传控件无法点击的BUG
        var upfile = $('#picker').children().eq(1);
        upfile.width(80);
        upfile.height(45);
    });


});

//加载目录
var updateTreeDir = function () {
    $.getJSON(doc.treeUrl, { t: new Date().getTime(), leaf: false }, function (data) {
        doc.$treeDir.treeview({ data: data, enableLinks: false });
        doc.$treeDir.treeview('expandAll', { levels: 4, silent: true });
        doc.$treeDir.on('nodeSelected', function (event, node) {
            $('#txtDir').val(node.path);
            doc.$txtDir.popover('hide');
        });
    });
};

var showModalErrorMsg = function (msg) {
    doc.$modalErrorMsg.text(msg);
    doc.$modalError.show();
}

var hideModalErrorMsg = function () {
    doc.$modalError.hide();
}


//文件上传 参考 http://fex.baidu.com/webuploader/
$(function () {
    var $list = $('#fileList'),
        $btn = $('#ctlBtn'),
        state = 'pending';
    var uploader = WebUploader.create({
        //swf文件路径
        swf: 'Res/Uploader/Uploader.swf',
        //文件接收服务器
        server: 'Doc/Up',
        pick: '#picker',
        resize: false,
        //文件类型
        accept: {
            title: 'PDF',
            extensions: 'pdf,PDF,doc,docx',
            mimeTypes: 'application/pdf,application/msword,application/vnd.openxmlformats-officedocument.wordprocessingml.document'
        }
    });

    //显示用户选择的文件
    uploader.on('fileQueued', function (file) {
        $list.append(
            '<div id="' + file.id + '" class="item">' +
            '<h4 class="info">' + file.name + '</h4>' +
            '<p class="state">等待上传...</p>' +
            '</div>'
        );
    });

    //上传成功
    uploader.on('uploadSuccess', function (file, response) {
        var msg = $('#' + file.id).find('p.state');
        if (response.IsSuccess) {
            msg.text('已上传');
        } else {
            msg.text(response.Msg);
            // TODO 错误处理
        }

    });

    //上传出错
    uploader.on('uploadError', function (file, reson) {
        $('#' + file.id).find('p.state').text('上传出错，错误码:' + reson);
    });

    //上传完成
    uploader.on('uploadComplete', function (file) {
        $('#' + file.id).find('.progress').fadeOut();
    });

    //文件上传过程中创建进度条实时显示
    uploader.on('uploadProgress', function (file, percentage) {
        var $li = $('#' + file.id),
            $percent = $li.find('.progress .progress-bar');
        //避免重复创建
        if (!$percent.length) {
            $percent = $(
                '<div class="progress progress-striped active">' +
                '<div class="progress-bar" role="progressbar" style="width:0%"></div>' +
                '</div>'
            ).appendTo($li).find('.progress-bar');
        }
        $li.find('p.state').text('上传中');
        $percent.css('width', percentage * 100 + '%');
    });

    uploader.on('all', function (type) {
        if (type === 'startUpload') {
            state = 'uploading';
        } else if (type === 'stopUpload') {
            state = 'paused';
        } else if (type === 'uploadFinished') {
            state = 'done';
        }

        if (state === 'uploading') {
            $btn.text('暂停上传');
        } else {
            $btn.text('开始上传');
        }
    });

    $btn.on('click', function () {
        if (state === 'uploading') {
            uploader.stop();
        } else {

            if (doc.$txtDir.val() === '') {
                doc.$txtDir.popover('show');
                return;
            }
            //附带数据
            uploader.options.formData.path = doc.$txtDir.val();
            uploader.upload();
        }
    });

});

var changeDoc = function (name) {
    name = 'docs' + name;
    $FlexPaper('documentViewer').loadSwf(name);
};

var setRootPath = function () {
    doc.$txtDir.val('/');
};