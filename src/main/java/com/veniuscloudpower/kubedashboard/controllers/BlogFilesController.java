package com.veniuscloudpower.kubedashboard.controllers;

import com.azure.storage.blob.BlobServiceClientBuilder;
import com.azure.storage.blob.models.ListBlobsOptions;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.core.io.*;
import org.springframework.http.HttpHeaders;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.util.StreamUtils;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RestController;


import org.springframework.beans.factory.annotation.Value;
import org.springframework.core.io.WritableResource;
import org.springframework.util.StreamUtils;
import org.springframework.web.bind.annotation.*;

import java.io.ByteArrayInputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.nio.charset.Charset;
import java.util.ArrayList;
import java.util.List;
import java.util.Locale;

@RestController
@RequestMapping(value ={ "api/files"})
public class BlogFilesController {

    @Autowired
    private BlobServiceClientBuilder blobServiceClientBuilder;


    @GetMapping("getFiles/{containerName}")
    public ResponseEntity<ArrayList<String>> getlistoffile(@PathVariable String containerName) throws IOException
    {
        var blogContainerService = blobServiceClientBuilder.buildClient().getBlobContainerClient(containerName);
        var listblogs = blogContainerService.listBlobs();
        var filelist = new ArrayList<String>();

        listblogs.forEach(item ->{
            filelist.add(item.getName());
        });

        return  ResponseEntity.ok().body(filelist);
    }


    @GetMapping("/getfile/{containerName}/{blobName}")
    public ResponseEntity<Resource> readBlobFile(@PathVariable String containerName,
                               @PathVariable String blobName) throws IOException {

        var streamdata = blobServiceClientBuilder.buildClient().getBlobContainerClient(containerName).getBlobClient(blobName).downloadContent().toBytes();
        ByteArrayResource resource = new ByteArrayResource(streamdata);

        HttpHeaders header = new HttpHeaders();
        header.add(HttpHeaders.CONTENT_DISPOSITION, "attachment; filename="+blobName);
        header.add("Cache-Control", "no-cache, no-store, must-revalidate");
        header.add("Pragma", "no-cache");
        header.add("Expires", "0");

        return ResponseEntity.ok()
                .headers(header)
                .contentLength(resource.contentLength())
                .contentType(MediaType.APPLICATION_OCTET_STREAM)
                .body(resource);

    }

    @PostMapping("/upload/{containerName}/{blobName}")
    public ResponseEntity<String> uploadfile (@PathVariable String containerName,
                                                @PathVariable String blobName,@RequestBody byte[] fileBytes)
    {
        var targetContainer = blobServiceClientBuilder.buildClient().getBlobContainerClient(containerName.toLowerCase());
        if(!targetContainer.exists()){
                 targetContainer.create();
        }
        InputStream dataStream = new ByteArrayInputStream(fileBytes);
        targetContainer.getBlobClient(blobName).upload(dataStream,fileBytes.length);
        return ResponseEntity.ok(targetContainer.getBlobClient(blobName).getBlobUrl());

    }








}
