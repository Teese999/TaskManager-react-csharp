import * as React from 'react';
import { DataGrid, GridActionsCellItem } from '@mui/x-data-grid';
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';
import Button from '@mui/material/Button';
import Typography from '@mui/material/Typography';
import axios from "axios";
import { useState } from 'react';
import TaskForm from './TaskForm';
import Settings from './Models/settings';
export default function Tasks(data) {
    const [openDialog, setOpenDialog] = useState(false);
    const [currentTaskId, setCurrentTaskId] = useState(false);

    const changeProjIdCallback = data.changeProjIdCallback;
    const removeTaskCallback = data.removeTaskCallback;
    const updateTaskCallback = data.updateTasksCallback;

    const ENDPOINT = Settings.ENDPOINT;

    const deleteTask =
        (id) => () => {
            removeTask(id);
            removeTaskCallback(id);
        };

    const editTask =
        (id) => () => {
            setCurrentTaskId(id);
            setOpenDialog(true);
        };

    const selectProject =
        (id) => () => {
            changeProjIdCallback(id);
        };

    async function removeTask(id) {
        const apiUrl = `${ENDPOINT}/Task/${id}`;
        await axios.delete(apiUrl).then(function (res) {

        })
            .catch(function (error) {
                console.log('error')
            })
    }

    return (
        <div>
            <div>
                <DataGrid
                    columns={[
                        { field: 'index', headerName: '#', },
                        { field: 'spendedTime', headerName: 'Times' },
                        {
                            field: 'projectName', headerName: 'Project', flex: 1, type: 'actions',
                            getActions: (params) => [
                                <Button onClick={selectProject(params.row.projectId)}>{params.row.projectName}</Button>
                            ]
                        },
                        { field: 'taskName', headerName: 'Task' },
                        { field: 'startDate', headerName: 'Start' },
                        { field: 'cancelDate', headerName: 'End', },
                        {
                            field: 'actions ',
                            type: 'actions',
                            width: 80,
                            headerName: 'Actions',
                            getActions: (params) => [
                                <GridActionsCellItem
                                    icon={<DeleteIcon />}
                                    label="Delete"
                                    onClick={deleteTask(params.id)}
                                />,
                                <GridActionsCellItem
                                    icon={<EditIcon />}
                                    label="Edit"
                                    onClick={editTask(params.id)}
                                />,

                            ]
                        },
                    ]}
                    rows={data.tasks}
                    autoHeight={true}

                />
                {
                    openDialog === true ? <TaskForm taskId={currentTaskId} openHandler={setOpenDialog} updateHandler={updateTaskCallback}></TaskForm> : <div></div>
                }


            </div>
            <div>
                <Typography variant="h2" component="h2">
                    Time Spended summary: {data.time}
                </Typography>

            </div>
        </div>

    );

};
