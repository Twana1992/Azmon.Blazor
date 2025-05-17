window.downloadFileFromStream = async (fileName, contentStreamReference) => {
    const arrayBuffer = await contentStreamReference.arrayBuffer();
    const blob = new Blob([arrayBuffer], { type: 'application/pdf' }); // مهم تحديد نوع الملف
    const url = URL.createObjectURL(blob);

    // فتح الملف في نافذة جديدة
    window.open(url, '_blank');

    // إلغاء الرابط من الذاكرة بعد فترة
    setTimeout(() => {
        URL.revokeObjectURL(url);
    }, 10000);
};

