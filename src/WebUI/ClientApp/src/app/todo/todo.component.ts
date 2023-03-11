import { Component, TemplateRef, OnInit } from '@angular/core';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { webapiclient,
  TodoListDto, TodoItemDto, PriorityLevelDto,
  CreateTodoListCommand, UpdateTodoListCommand,
  CreateTodoItemCommand, UpdateTodoItemCommand, UpdateTodoItemDetailCommand
} from '../webapiclient';

@Component({
  selector: 'app-todo-component',
  templateUrl: './todo.component.html',
  styleUrls: ['./todo.component.scss']
})
export class TodoComponent implements OnInit {
  debug = false;
  lists: TodoListDto[];
  priorityLevels: PriorityLevelDto[];
  selectedList: TodoListDto;
  selectedItem: TodoItemDto;
  newListEditor: any = {};
  listOptionsEditor: any = {};
  itemDetailsEditor: any = {};
  newListModalRef: BsModalRef;
  listOptionsModalRef: BsModalRef;
  deleteListModalRef: BsModalRef;
  itemDetailsModalRef: BsModalRef;

  constructor(
    private listsClient: webapiclient,
    private itemsClient: webapiclient,
    private modalService: BsModalService
  ) {}

  ngOnInit(): void {
    this.listsClient.getTodoListsEndpoint().subscribe(
      result => {
        this.lists = result.lists;
        this.priorityLevels = result.priorityLevels;
        if (this.lists.length) {
          this.selectedList = this.lists[0];
        }
      },
      error => console.error(error)
    );
  }

  // Lists
  remainingItems(list: TodoListDto): number {
    return list.items.filter(t => !t.done).length;
  }

  showNewListModal(template: TemplateRef<any>): void {
    this.newListModalRef = this.modalService.show(template);
    setTimeout(() => document.getElementById('title').focus(), 250);
  }

  newListCancelled(): void {
    this.newListModalRef.hide();
    this.newListEditor = {};
  }

  addList(): void {
    const list = {
      id: 0,
      title: this.newListEditor.title,
      items: []
    } as TodoListDto;

    this.listsClient.createTodoListEndpoint(list as CreateTodoListCommand).subscribe({
        next: result => {
          list.id = result.id;
          this.lists.push(list);
          this.selectedList = list;
          this.newListModalRef.hide();
          this.newListEditor = {};
      },
      error: error => {
        const errors = JSON.parse(error.response);

        if (errors && errors.Title) {
          this.newListEditor.error = errors.Title[0];
        }

        setTimeout(() => document.getElementById('title').focus(), 250);
      }
    });
  }

  showListOptionsModal(template: TemplateRef<any>) {
    this.listOptionsEditor = {
      id: this.selectedList.id,
      title: this.selectedList.title
    };

    this.listOptionsModalRef = this.modalService.show(template);
  }

  updateListOptions() {
    const list = this.listOptionsEditor as UpdateTodoListCommand;
    this.listsClient.updateTodoListEndpoint(list, this.selectedList.id).subscribe(
      () => {
        (this.selectedList.title = this.listOptionsEditor.title),
          this.listOptionsModalRef.hide();
        this.listOptionsEditor = {};
      },
      error => console.error(error)
    );
  }

  confirmDeleteList(template: TemplateRef<any>) {
    this.listOptionsModalRef.hide();
    this.deleteListModalRef = this.modalService.show(template);
  }

  deleteListConfirmed(): void {
    this.listsClient.deleteTodoListEndpoint(this.selectedList.id).subscribe(
      () => {
        this.deleteListModalRef.hide();
        this.lists = this.lists.filter(t => t.id !== this.selectedList.id);
        this.selectedList = this.lists.length ? this.lists[0] : null;
      },
      error => console.error(error)
    );
  }

  // Items
  showItemDetailsModal(template: TemplateRef<any>, item: TodoItemDto): void {
    this.selectedItem = item;
    this.itemDetailsEditor = {
      ...this.selectedItem
    };

    this.itemDetailsModalRef = this.modalService.show(template);
  }

  updateItemDetails(): void {
    const item = this.itemDetailsEditor as UpdateTodoItemDetailCommand;
    this.itemsClient.updateItemDetailsEndpoint(item, this.selectedItem.id).subscribe(
      () => {
        if (this.selectedItem.listId !== this.itemDetailsEditor.listId) {
          this.selectedList.items = this.selectedList.items.filter(
            i => i.id !== this.selectedItem.id
          );
          const listIndex = this.lists.findIndex(
            l => l.id === this.itemDetailsEditor.listId
          );
          this.selectedItem.listId = this.itemDetailsEditor.listId;
          this.lists[listIndex].items.push(this.selectedItem);
        }

        this.selectedItem.priority = this.itemDetailsEditor.priority;
        this.selectedItem.note = this.itemDetailsEditor.note;
        this.itemDetailsModalRef.hide();
        this.itemDetailsEditor = {};
      },
      error => console.error(error)
    );
  }

  addItem() {
    const item = {
      id: 0,
      listId: this.selectedList.id,
      priority: this.priorityLevels[0].value,
      title: '',
      done: false
    } as TodoItemDto;

    this.selectedList.items.push(item);
    const index = this.selectedList.items.length - 1;
    this.editItem(item, 'itemTitle' + index);
  }

  editItem(item: TodoItemDto, inputId: string): void {
    this.selectedItem = item;
    setTimeout(() => document.getElementById(inputId).focus(), 100);
  }

  updateItem(item: TodoItemDto, pressedEnter: boolean = false): void {
    const isNewItem = item.id === 0;

    if (!item.title.trim()) {
      this.deleteItem(item);
      return;
    }

    if (item.id === 0) {
      this.itemsClient
        .createTodoItemEndpoint({ title: item.title, listId: this.selectedList.id
          } as CreateTodoItemCommand)
        .subscribe(
          result => {
            item.id = result.id;
          },
          error => console.error(error)
        );
    } else {
      this.itemsClient.updateItemDetailsEndpoint(item as UpdateTodoItemCommand, this.selectedItem.id).subscribe(
        () => console.log('Update succeeded.'),
        error => console.error(error)
      );
    }

    this.selectedItem = null;

    if (isNewItem && pressedEnter) {
      setTimeout(() => this.addItem(), 250);
    }
  }

  deleteItem(item: TodoItemDto) {
    if (this.itemDetailsModalRef) {
      this.itemDetailsModalRef.hide();
    }

    if (item.id === 0) {
      const itemIndex = this.selectedList.items.indexOf(this.selectedItem);
      this.selectedList.items.splice(itemIndex, 1);
    } else {
      this.itemsClient.deleteTodoItemEndpoint(item.id).subscribe(
        () =>
          (this.selectedList.items = this.selectedList.items.filter(
            t => t.id !== item.id
          )),
        error => console.error(error)
      );
    }
  }
}
