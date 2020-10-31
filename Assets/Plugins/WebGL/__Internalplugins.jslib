mergeInto(LibraryManager.library, {

  Hello: function () {
    console.log("Hello, world!");
  },
  //提交成绩，type 1 2 3 为实训ID SUCCESS为成绩
  Submit: function (type, success) {
    writeSuccess(type, success);
  },
  //获取用户信息
  GetLoginInfo: function () {
    var returnStr = getLogin();
    var bufferSize = lengthBytesUTF8(returnStr) + 1;
    var buffer = _malloc(bufferSize);
    stringToUTF8(returnStr, buffer, bufferSize);
    return buffer;
  },
  //打开新的网页
  ToLinkTargetUrl: function (data) {
    linkTargetUrl(Pointer_stringify(data));
  },
  //提交操作记录
  DataUpload:function(experiment_num, exName, data)
  {
	var _exName = Pointer_stringify(exName);
	var _exData = Pointer_stringify(data);
	writeDataUpload(experiment_num, _exName, _exData);
  },
   //工厂漫游提交分数
  UpdateFactoryScore:function(timer)
  {
	console.log("updateFactoryScore:" + timer);
	updateFactoryScore(timer);
  },
  
  //工厂漫游视频播放进度暂存
  SaveFactoryProcess:function(videoType, process)
  {
	var _videoType = Pointer_stringify(videoType);
	saveFactoryProcess(_videoType, process);
  },
  
  //获取工厂漫游视频类型
  GetFactoryVideoType:function()
  {
	var returnStr = getFactoryVideoType();
    var bufferSize = lengthBytesUTF8(returnStr) + 1;
    var buffer = _malloc(bufferSize);
    stringToUTF8(returnStr, buffer, bufferSize);
    return buffer;
  },
    
  //获取工厂漫游视频播放进度暂存
  GetFactoryProcess:function()
  {
	return getFactoryProcess();
  },
  
});