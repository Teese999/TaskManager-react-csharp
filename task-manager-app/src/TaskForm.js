import React, { useState, useEffect } from 'react';
import axios from "axios";
import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogTitle from '@mui/material/DialogTitle';
import TextField from '@mui/material/TextField';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';
import { DateTimePicker } from '@mui/x-date-pickers/DateTimePicker';
import ToggleButton from "@mui/material/ToggleButton";
import ToggleButtonGroup from "@mui/material/ToggleButtonGroup";
import { Box } from '@mui/material';
import Button from '@mui/material/Button';
import { DataGrid, GridActionsCellItem } from '@mui/x-data-grid';
import DeleteIcon from '@mui/icons-material/Delete';
import Settings from "./Models/settings";

const dayjs = require('dayjs')


export default function TaskForm(data) {
    const [currentTask, setCurrentTask] = useState('');
    const [currentComments, setComments] = useState([]);
    const [openDialog, setOpenDialog] = useState(true);
    const [openCommentView, setOpenCommentView] = useState(false);
    const openHandler = data.openHandler;
    const [currentAligment, setCurrentAligment] = useState(1)
    const updateCallback = data.updateHandler;

    const ENDPOINT = Settings.ENDPOINT;


    function mapComments(comments) {
        comments.map(obj => {
            switch (obj.commentType) {
                case 0:
                    obj.content = obj.file
                    break;
                case 1:
                    obj.content = obj.text
                    break;
                default:
                    break;
            }
        })
        setComments(comments);
    }
    const commentFormData = new FormData();
    const taskFormData = new FormData();

    commentFormData.set('CommentType', currentAligment);
    commentFormData.set('Text', '');
    commentFormData.set('File', '');
    taskFormData.set('TaskName', currentTask.taskName);
    taskFormData.set('Id', currentTask.id);
    taskFormData.set('StartDate', dayjs(currentTask.startDate).toJSON());
    taskFormData.set('CancelDate', dayjs(currentTask.cancelDate).toJSON());
    const getTask = async () => {
        await axios.get(`${ENDPOINT}/Task/${data.taskId}`).then(res => {
            setCurrentTask(res.data);

        })
    }
    const getComments = async () => {
        await axios.get(`${ENDPOINT}/Comment/GetByTaskId/${data.taskId}`).then(res => {
            mapComments(res.data);
        })
    }
    useEffect(() => {
        getTask();
        getComments();
    }, [])
    const getFileCallback = React.useCallback(
        (id) => async () => {
            let fileName = currentTask.taskComments.find(x => x.id === id).fileName;

            await axios.get(`${ENDPOINT}/Comment/file/${id}`, { responseType: 'blob' }).then(({ data }) => {

                const downloadUrl = window.URL.createObjectURL(new Blob([data]));

                const link = document.createElement('a');

                link.href = downloadUrl;

                link.setAttribute('download', fileName); //any other extension

                document.body.appendChild(link);

                link.click();

                link.remove();
            })
        },

    );
    const deleteComment = (id) => async () => {
        await axios.delete(`${ENDPOINT}/Comment/${id}`).then(res => {
            getComments();
        })
    };
    const sendComment = async () => {
        commentFormData.set('TaskId', currentTask.id);
        commentFormData.set('CommentType', currentAligment);
        await axios.post(`${ENDPOINT}/Comment/`, commentFormData);
        toogleCommentPlace();
        commentFormData.set('Text', '');
        commentFormData.set('File', '');
        getComments();
        getTask();

    };

    const updateTaskData = async () => {
        taskFormData.set('StartDate', currentTask.startDate);
        taskFormData.set('CancelDate', currentTask.cancelDate);

        await axios.put(`${ENDPOINT}/Task/`, taskFormData);
        getComments();
        getTask();
        updateCallback()
        closeDialog();
    };
    const closeDialog = () => {
        setOpenDialog(false);
        openHandler(false);
    }
    const toogleCommentPlace = () => {
        setOpenCommentView(!openCommentView);
    }

    function redterContent(params) {
        switch (params.row.commentType) {
            case 0:
                return <div>{params.row.text}</div>
            case 1:

                return <Button variant='outlined' onClick={getFileCallback(params.row.id)}>Download</Button>
            default:
                break;
        }
    }
    return currentTask.taskName !== undefined ?
        <div>
            <Dialog open={openDialog} onClose={() => { closeDialog() }}>
                <DialogTitle>Update task {currentTask.taskName}</DialogTitle>
                <DialogContent sx={{
                    display: 'flex',
                    flexDirection: 'column',
                    margin: '50px',
                    alignItems: 'flex-start',
                    justifyContent: 'space-around',
                }}>
                    <TextField
                        autoFocus
                        margin="dense"
                        label="TaskName"
                        type="text"
                        fullWidth
                        defaultValue={currentTask.taskName}
                        variant="standard"
                        onChange={(event) => {
                            taskFormData.set('TaskName', event.target.value)
                        }}>
                    </TextField>
                    <Box sx={{ display: "flex", flexDirection: "row", paddingTop: "20px" }}>
                        <LocalizationProvider dateAdapter={AdapterDayjs} >
                            <DateTimePicker

                                renderInput={(props) => <TextField {...props} />}
                                label="Start Time"
                                value={currentTask.startDate}
                                onChange={(newValue) => {
                                    setCurrentTask({ ...currentTask, ['startDate']: dayjs(newValue).toJSON() });

                                }}
                            />
                        </LocalizationProvider>
                        <LocalizationProvider dateAdapter={AdapterDayjs}>
                            <DateTimePicker
                                renderInput={(props) => <TextField {...props} />}

                                label="Сancel TIme"
                                value={currentTask.cancelDate}
                                onChange={(newValue) => {
                                    setCurrentTask({ ...currentTask, ['cancelDate']: dayjs(newValue).toJSON() });
                                }}
                            />
                        </LocalizationProvider>

                    </Box>

                    <Button variant='contained' fullWidth onClick={toogleCommentPlace}>Add Comment</Button>

                    {openCommentView ?
                        <Box sx={{ width: '100%', paddingTop: '20px', paddingBottom: '20px', textAlign: 'center' }} >
                            <ToggleButtonGroup
                                fullWidth
                                color="primary"
                                exclusive
                                value={currentAligment}
                                onChange={(event, newAlignment) => {
                                    if (newAlignment !== null) {
                                        setCurrentAligment(newAlignment);
                                    }

                                }}
                                aria-label="Platform"
                            >
                                <ToggleButton value={1}>File</ToggleButton>
                                <ToggleButton value={0}>Text</ToggleButton>
                            </ToggleButtonGroup>
                            {
                                currentAligment === 1 ? <TextField
                                    margin="dense"
                                    fullWidth
                                    type="file"
                                    onChange={(e) => {
                                        if (e.target.files.length > 0) {
                                            commentFormData.set('File', e.target.files[0], e.target.files[0].name)
                                        }
                                    }}
                                /> : <TextField
                                    label="Description"
                                    margin="dense"
                                    fullWidth
                                    type="text"
                                    onChange={(e) => {
                                        commentFormData.set('Text', e.target.value)
                                    }}
                                />

                            }
                            <Button variant='contained' sx={{ width: '50%', }} onClick={sendComment}>Send</Button>
                        </Box> : <div></div>}
                    <Box sx={{ width: '100%' }}>
                        <DataGrid
                            columns={[
                                {

                                    field: 'content',
                                    headerName: 'Content',
                                    flex: 1,
                                    renderCell: redterContent,

                                },
                                {
                                    field: 'actions ',
                                    type: 'actions',
                                    width: 80,
                                    headerName: 'Remove',
                                    getActions: (params) => [
                                        <GridActionsCellItem
                                            icon={<DeleteIcon />}
                                            label="Delete"
                                            onClick={deleteComment(params.id)}
                                        />,

                                    ]
                                },
                            ]}
                            rows={currentComments}
                            autoHeight={true}
                        />

                    </Box>
                </DialogContent>
                <DialogActions>
                    <Button onClick={() => { closeDialog() }}>Cancel</Button>
                    <Button onClick={updateTaskData}>update</Button>
                </DialogActions>
            </Dialog>
        </div>
        : <div>Loading</div>

}