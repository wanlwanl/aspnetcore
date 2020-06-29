// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

package com.microsoft.signalr.sample;

import java.util.concurrent.ArrayBlockingQueue;
import java.util.concurrent.ThreadPoolExecutor;
import java.util.concurrent.TimeUnit;

import com.microsoft.signalr.HubConnection;
import com.microsoft.signalr.HubConnectionBuilder;

public class Chat {
    public static void main(String[] args) {
        while (true)
        {
            try {
                ThreadPoolExecutor executor = new ThreadPoolExecutor(100, 200, 300,
                        TimeUnit.MILLISECONDS, new ArrayBlockingQueue<>(3),
                        new ThreadPoolExecutor.CallerRunsPolicy());
                HubConnection hubConnection = HubConnectionBuilder.create("http://localhost:5000/default").build();
                for(int i=0;i<20;i++) start(executor, hubConnection);
                Thread.sleep(1000);
                executor.shutdown();
                Thread.sleep(1000);
                hubConnection.stop().blockingAwait();
            } catch (InterruptedException e) {
                e.printStackTrace();
            }
        }
    }

    public static void start(ThreadPoolExecutor executor, HubConnection hubConnection) {
        executor.execute(() -> {
            while (true) {
                try {
                    hubConnection.start();
                    Thread.sleep((int) (Math.random() * 100));
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });
    }
}
